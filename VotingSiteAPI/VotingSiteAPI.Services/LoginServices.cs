using AutoMapper;
using System;
using System.Diagnostics;
using System.Linq;
using VotingSiteAPI.Data.Enums;
using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IElectionsRepository _electionsRepo;
        private readonly IVotesRepository _votesRepository;
        private readonly IVotersRepository _votersRepo;
        private readonly ILoginAttemptsRepository _loginAttemptsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginServices"/> class.
        /// </summary>
        /// <param name="electionsRepo">The elections repo.</param>
        /// <param name="votesRepository">
        /// <see cref="IVotesRepository"/>
        /// </param>
        /// <param name="votersRepo">The voters repo.</param>
        /// <param name="loginAttemptsRepo">The login attempts repo.</param>
        /// <exception cref="ArgumentNullException">
        /// electionsRepo
        /// or
        /// votersRepo
        /// or
        /// loginAttemptsRepo
        /// </exception>
        public LoginServices(
            IElectionsRepository electionsRepo,
            IVotesRepository votesRepository,
            IVotersRepository votersRepo,
            ILoginAttemptsRepository loginAttemptsRepo)
        {
            _electionsRepo = electionsRepo ?? throw new ArgumentNullException(nameof(electionsRepo));
            _votesRepository = votesRepository ?? throw new ArgumentNullException(nameof(votesRepository));
            _votersRepo = votersRepo ?? throw new ArgumentNullException(nameof(votersRepo));
            _loginAttemptsRepo = loginAttemptsRepo ?? throw new ArgumentNullException(nameof(loginAttemptsRepo));
        }

        /// <summary>
        /// Gets an appropriately hydrated <see cref="Election"/> instance,
        /// then maps it to and returns an instance of the
        /// <see cref="RetrievedPageDataModel"/>
        /// </summary>
        /// <returns></returns>
        public RetrievedPageDataModel GetPreLoginElectionData(int electionId)
        {
            // get the election data
            var election = _electionsRepo.GetById(electionId);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Election, RetrievedPageDataModel>()
                    .ForMember(
                        dest => dest.ElectionId,
                        opt => opt.MapFrom(src => src.Id));
            });

            // map it to a RetrievedPageDataModel
            var iMapper = mapperConfig.CreateMapper();
            RetrievedPageDataModel result = iMapper.Map<Election, RetrievedPageDataModel>(election);

            return result;
        }

        /// <summary>
        /// Checks the credentials entered by the user and returns true or
        /// false based on whether or not those credentials match what's in
        /// the database.
        /// <para>
        /// This method will also hydrate the <c>VoterId</c> field of the
        /// passed-in <c>UserCredentialsModel</c> if the Voter was found.
        /// </para>
        /// </summary>
        /// <param name="userCredentials">
        /// An instance of the <see cref="UserCredentialsModel"/> with its 
        /// </param>
        /// <returns></returns>
        public bool ValidateUserCredentials(UserCredentialsModel userCredentials)
        {
            var foundVoter = _votersRepo.Get(
                vtf => vtf.LoginId == userCredentials.UsernameOrId &&
                       vtf.LoginPin == userCredentials.PasswordOrPin &&
                       vtf.ElectionId == userCredentials.ElectionId);

            if (foundVoter == null)
            {
                return false;
            }

            userCredentials.VoterId = foundVoter.Id;

            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempt to Log the user in, as well as logging the attempt.
        /// </summary>
        /// <param name="ivrUserCredentials">
        /// An instance of the user's credentials via the
        /// <see cref="IvrUserCredentialsInputModel"/> class.
        /// </param>
        /// <param name="browserAgent">
        /// A string containing... for the logging bit.
        /// </param>
        /// <param name="userIpAddress">
        /// A string containing... for the logging bit.
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="IvrUserLoginResponseModel"/>
        /// class.
        /// </returns>
        public IvrUserLoginResponseModel OrchestrateIvrUserLoginAndAttempt(
            IvrUserCredentialsInputModel ivrUserCredentials,
            string browserAgent,
            string userIpAddress)
        {
            var result = new IvrUserLoginResponseModel { AuthResult = -1 };

            if (ivrUserCredentials == null)
            {
                throw new ArgumentNullException(nameof(ivrUserCredentials));
            }

            // convert IvrUserCredentialsInputModel > UserCredentialsModel
            var ucm = new UserCredentialsModel
            {
                UsernameOrId = ivrUserCredentials.PIN,
                PasswordOrPin = ivrUserCredentials.SSN,
                ElectionId = ivrUserCredentials.ElectionID,
                VoterId = -1
            };

            bool? accountLockedOut = null;
            try
            {
                // check account lockout status
                accountLockedOut = IsAccountLockedOut(ucm);
                if (accountLockedOut == null)
                {
                    // TODO: Should probably make an enum or three for these 'magic numbers' -SKF 6/9/19
                    // specified user wasn't found, so returning 'Invalid' as the status
                    result.AuthResult = (int)IvrLoginStatusCodes.InvalidCredentials;
                    return result;
                }

                // if locked out, bail with appropriate code
                if (accountLockedOut.Value)
                {
                    // specified user's account is locked out
                    result.AuthResult = (int)IvrLoginStatusCodes.AccountLockedOut;
                    return result;
                }

                // check specified user credentials; is voter found, etc.
                // "Login info Correct?" (From the CalPERS logic diagram for the IVR system)
                if (!ValidateUserCredentials(ucm))
                {
                    // if not ok, bail with appropriate code 
                    result.AuthResult = (int)IvrLoginStatusCodes.InvalidCredentials;
                    return result;
                }

                // check to see if voter's already voted
                // if so, again, bail with appropriate code 
                var votesCastForThisElection = _votesRepository
                    .GetMany(vote => vote.VoterId.Equals(ucm.VoterId)).Any();

                if (votesCastForThisElection)
                {
                    // ...If so, return a value indicating that the voter's
                    // already voted.
                    result.AuthResult = (int)IvrLoginStatusCodes.AlreadyVoted;
                    return result;
                }

                // login credentials verified
                result.AuthResult = ucm.VoterId;
            }
            catch (Exception oEx)
            {
                Debug.WriteLine(oEx);

                throw;
            }
            finally
            {
                // log the login attempt whether or not it was successful.
                var loginAttempt = new LoginAttempt
                {
                    BrowserAgent = browserAgent,
                    UserIp = userIpAddress,
                    TimeStamp = DateTime.Now,
                    EnteredLoginId = ucm.UsernameOrId,
                    SuccessfulLogin = result.AuthResult > 0
                };

                _loginAttemptsRepo.Add(loginAttempt);
                var modifiedRecords = 1;
#if DEBUG
                _votesRepository.GetDbContext().Database.Log = generatedSql =>
                    Debug.WriteLine("Generated SQL Query:\r\n" + generatedSql);
#endif
                // this is necessary because LINQ to Entities doesn't grok 
                // DateTime.Now.AddMinutes()
                var nowMinus20 = DateTime.Now.AddMinutes(-20);

                // check number of attempts in last 20 minutes [per "Phone IVR Voting Login Process" diagram]
                var loginAttemptsByPIN = _loginAttemptsRepo.GetMany(la => 
                        la.EnteredLoginId == ucm.UsernameOrId &&
                        la.SuccessfulLogin == false &&
                        la.TimeStamp >= nowMinus20).Count();

                Voter voter = null;
                voter = result.AuthResult > 0
                    ? _votersRepo.GetById(ucm.VoterId)
                    : _votersRepo.Get(v => v.LoginId == ucm.UsernameOrId);

                // the number of attempts we're looking for is 4, but since
                // the first attempt won't have been written to the db yet,
                // I've got this set to 3. -SKF 6/10/19
                if (voter != null)
                {
                    // if we had to use ucm.UsernameOrId to find the Voter, it is
                    // possible they don't exist because the user could have easily
                    // entered a bad 'Username' | PIN.

                    if (loginAttemptsByPIN > 3)
                    {
                        // if they've already got a AccountLockoutExpires
                        // value, we're not trying to extend it.
                        if (!voter.AccountLockoutExpires.HasValue)
                        {
                            // lock 'em out!
                            voter.AccountLockoutExpires = DateTime.Now.AddMinutes(29.9);
                            _votersRepo.Update(voter);
                            modifiedRecords++;
                        }
                    }

                    if (loginAttemptsByPIN <= 3 &&
                        accountLockedOut.HasValue && !accountLockedOut.Value &&
                        voter.AccountLockoutExpires != null)
                    {
                        // clear the lockout expiration DateTime
                        voter.AccountLockoutExpires = null;
                        _votersRepo.Update(voter);
                        modifiedRecords++;
                    }
                }

                // save above change(s)
                var recordsModified = _votesRepository.GetDbContext().SaveChanges();
                if (recordsModified < modifiedRecords)
                {
                    Debug.WriteLine($"There may have been a failure to write User {loginAttempt.EnteredLoginId}'s Login Attempt or lockout status.");
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether [is account locked out] via [the specified
        /// user credentials].
        /// <para>
        /// If the user is not found by the 'Username' entered, then this
        /// method returns null.
        /// </para>
        /// </summary>
        /// <param name="userCredentials">
        /// An instance of the user credentials model class.
        /// (<see cref="UserCredentialsModel"/>)
        /// </param>
        /// <returns>
        /// If the user is not found by the 'Username' entered, then this
        /// method returns null. Otherwise, the boolean value indicates
        /// whether (true) or not (false) the specified user's account is
        /// currently locked out.
        /// </returns>
        public bool? IsAccountLockedOut(UserCredentialsModel userCredentials)
        {
            // Leaving the 'password' out of the query so that I'll retrieve
            // user(s) who've entered the correct 'PIN' (which is the
            // 'Username' in the CalPERS Voting system), but possibly an 
            // incorrect 'password.' ('SSN' in the CalPERS app)
            var foundVoter = _votersRepo.Get(
                vtf => vtf.LoginId == userCredentials.UsernameOrId &&
                       vtf.ElectionId == userCredentials.ElectionId);

            if (foundVoter == null)
            {
                return null;
            }

            var lockedOut = foundVoter.AccountLockoutExpires != null &&
                            foundVoter.AccountLockoutExpires > DateTime.Now;

            return lockedOut;
        }


    }
}
