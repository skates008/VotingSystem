using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

using AutoMapper;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services.Exceptions;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Services
{
    public class VotesServices : IVotesServices
    {
        private readonly IVotesRepository _votesRepository;
        private readonly IVotersRepository _votersRepository;
        private readonly ICandidatesRepository _candidatesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VotesServices"/> class.
        /// </summary>
        /// <param name="votesRepository">The votes repository.</param>
        /// <param name="votersRepository">The voters repository.</param>
        /// <param name="candidatesRepository">
        /// An instance of any concrete class that implements the
        /// <see cref="ICandidatesRepository"/> interface. This is typically
        /// the <see cref="CandidatesRepository"/> class.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// votesRepository
        /// or
        /// votersRepository
        /// </exception>
        public VotesServices(
            IVotesRepository votesRepository,
            IVotersRepository votersRepository,
            ICandidatesRepository candidatesRepository)
        {
            _votesRepository = votesRepository ?? throw new ArgumentNullException(nameof(votesRepository));
            _votersRepository = votersRepository ?? throw new ArgumentNullException(nameof(votersRepository));
            _candidatesRepository =
                candidatesRepository ?? throw new ArgumentNullException(nameof(candidatesRepository));

            VoteEqualityComparer = new VoteEqualityComparer();
        }

        /// <summary>
        /// Gets the Vote Equality Comparer.
        /// </summary>
        /// <remarks>
        /// This class can be tested by itself, and providing access to an
        /// instance within this class should be quite helpful.
        /// </remarks>
        public virtual IVoteEqualityComparer VoteEqualityComparer { get; }

        /// <summary>
        /// Performs the vote recording.
        /// </summary>
        /// <param name="recordVotesInput">The record votes input.</param>
        /// <returns>
        /// Currently set as an 'object', which is a place-holder for an 
        /// object that will contain the status of this 'Voting.'
        /// <para>
        /// The object returned does contain an integer <c>VotingSuccessful</c>
        /// property, which should be a 0 if all goes well.
        /// </para>
        /// </returns>
        public IvrVotingStatusModel OrchestrateVoteRecording(RecordVotesInputModel recordVotesInput)
        {
            var results = new IvrVotingStatusModel();
            var voterId = recordVotesInput.VoterId;

            try
            {
                var voter = _votersRepository.GetById(voterId);
                if (voter == null)
                {
                    // This should never happen because otherwise, how would
                    // this person have even been able to login?!
                    // If it has happened, then most likely there is a
                    // database issue of some kind.
                    results.VotingSuccessful = -1;
                    results.Exception = new VoterNotFoundException(
                        $"The Voter with the Id of {voterId} was not found in the system.");

                    return results;
                }

                // pull any votes cast already
                var votesCastForThisElection = _votesRepository
                    .GetMany(vote => vote.VoterId.Equals(voterId)).ToList();

                if (votesCastForThisElection.Any())
                {
                    // make sure the votesCastForThisElection haven't already
                    // been cast. If so, return a value indicating that the 
                    // voter's already voted.
                    results.VotingSuccessful = -2;

                    return results;
                }

                IEnumerable<Vote> mappedVotesToCast = MapCastVotesToVotes(recordVotesInput);

                try
                {
                    var votesRecordedSuccessfully = RecordIvrVotes(voter, mappedVotesToCast.ToList());

                    results.VotingSuccessful = votesRecordedSuccessfully ? 0 : -4;
                }
                catch (OptimisticConcurrencyException oce)
                {
                    Console.WriteLine(oce);

                    results.VotingSuccessful = -3;
                    results.Exception = oce;
                }
                catch (Exception oEx)
                {
                    Console.WriteLine(oEx);

                    results.VotingSuccessful = -3;
                    results.Exception = oEx;
                }

            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx);

                results.VotingSuccessful = -3;
                results.Exception = oEx;
            }

            return results;
        }

        /// <summary>
        /// Maps the collection (List&lt;VoteToRecord&gt;) of cast votes to
        /// a collection of <see cref="Vote"/> entities. (IEnumerable&lt;Vote&gt;)
        /// </summary>
        /// <param name="recordVotesInput">The record votes input.</param>
        /// <returns></returns>
        public virtual IEnumerable<Vote> MapCastVotesToVotes(
            RecordVotesInputModel recordVotesInput)
        {
            var voterId = recordVotesInput.VoterId;

            ////public class VoteToRecord
            ////    public int CandidateID { get; set; }
            ////    public string VoteDate { get; set; }

            // first, convert a VoteToRecord to a Vote entity, or, in this
            // case, the collection.

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VoteToRecord, Vote>()
                    .BeforeMap((src, dest) =>
                    {
                        dest.VoterId = voterId;
                        dest.CandidateId = src.CandidateID;
                        dest.Candidate = _candidatesRepository.GetById(src.CandidateID);
                        dest.Voter = _votersRepository.GetById(voterId);
                    })
                    .ForMember(
                        dest => dest.Id,
                        opt => opt.MapFrom(src => src.CandidateID));
            });

            var iMapper = mapperConfig.CreateMapper();
            var votesToRecord =
                iMapper.Map<List<VoteToRecord>, IEnumerable<Vote>>(recordVotesInput.VotesToRecord);

            return votesToRecord;
        }

        /// <summary>
        /// Records the votes sent via the IVR system.
        /// </summary>
        /// <param name="voter">
        /// An instance of the <see cref="Voter"/> class containing the
        /// 'current' voter.
        /// </param>
        /// <param name="votesToRecord">
        /// A List&lt;Vote&gt; containing the votes to save to the database.
        /// </param>
        /// <returns>
        /// A boolean value indicating whether (true) or not (false) the Votes
        /// were all recorded.
        /// </returns>
        public virtual bool RecordIvrVotes(
            Voter voter,
            List<Vote> votesToRecord)
        {
            var votesToRecordCount = 0;

            foreach (var vote in votesToRecord)
            {
                var newVote = vote;
                _votesRepository.Add(newVote);
                votesToRecordCount++;
            }

            // Mark Voting as Complete!
            voter.VoteCompleted = true;
            _votersRepository.Update(voter);

            // since all of the tables are within the single DbContext
            // (IVotingSiteAPIDbCtx/VotingSiteAPIDbCtx), it's okay to 
            // use the Votes repository [in this context] in order to
            // get/use an instance of same.
            // This should write the Votes AND the Voter to the database 
            // within a transaction. So, if either write fails, both should.
            var numChanges = _votesRepository.GetDbContext().SaveChanges();

            // the /+ 1/ is for the Voter
            return numChanges == votesToRecordCount + 1;
        }
    }

    /// <summary>
    /// This is an implementation of an <see cref="IEqualityComparer{Vote}"/>
    /// for testing equality of <see cref="Vote"/> instances.
    /// </summary>
    /// <seealso cref="VotingSiteAPI.Services.IVoteEqualityComparer" />
    public class VoteEqualityComparer : IVoteEqualityComparer
    {
        public bool Equals(Vote voteA, Vote voteB)
        {
            if (voteA == null && voteB == null)
            {
                return true;
            }

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (voteA == null && voteB != null)
            {
                return false;
            }

            if (voteA != null && voteB == null)
            {
                return false;
            }

            return voteA.CandidateId.Equals(voteB.CandidateId) &&
                   voteA.VoteDate.Equals(voteB.VoteDate);
        }

        public int GetHashCode(Vote voteToHash)
        {
            var simDateHash = voteToHash.VoteDate.Year +
                              voteToHash.VoteDate.Month +
                              voteToHash.VoteDate.Day +
                              voteToHash.VoteDate.Hour +
                              voteToHash.VoteDate.Minute +
                              voteToHash.VoteDate.Second +
                              voteToHash.VoteDate.Millisecond;

            return voteToHash.CandidateId + voteToHash.VoterId + simDateHash;
        }
    }
}
