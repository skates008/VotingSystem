
using System;

using AutoMapper;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.SharedModels;

namespace VotingSiteAPI.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IElectionsRepository _electionsRepo;
        private readonly IVotersRepository _votersRepo;

        // TODO: do I need Owin on this side too? -SKF 5/6/19
        public LoginServices(
            IElectionsRepository electionsRepo,
            IVotersRepository votersRepo)
        {
            _electionsRepo = electionsRepo ?? throw new ArgumentNullException(nameof(electionsRepo));
            _votersRepo = votersRepo ?? throw new ArgumentNullException(nameof(votersRepo));
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

            // TODO: LOG THIS Login Attempt!  (Doesn't that break SRP?)

            if (foundVoter == null)
            {
                return false;
            }

            userCredentials.VoterId = foundVoter.Id;

            return true;
        }
    }
}
