using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public class ContestServices : IContestServices
    {
        private readonly IContestsRepository _contestsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContestServices"/> class.
        /// </summary>
        /// <param name="contestsRepository">The contests repository.</param>
        /// <exception cref="ArgumentNullException">contestsRepository</exception>
        public ContestServices(
            IContestsRepository contestsRepository)
        {
            _contestsRepository = contestsRepository ?? throw new ArgumentNullException(nameof(contestsRepository));
        }

        /// <inheritdoc />
        /// <summary>
        /// Get's the <see cref="Contest"/>'s for the specified <see cref="Election"/>
        /// </summary>
        /// <param name="electionId">
        /// An integer containing the <c>ElectionId</c> for which its contests
        /// must be retrieved.
        /// </param>
        /// <returns>
        /// IEnumerable&lt;Contest&gt;
        /// </returns>
        public IEnumerable<Contest> GetContestsByElectionId(
            int electionId)
        {
            var retrievedContests = _contestsRepository.GetContestsByElectionId(electionId);

            return retrievedContests;
        }

        /// <summary>
        /// Gets the contests for ivr system.
        /// </summary>
        /// <param name="electionId">The election identifier.</param>
        /// <returns>
        /// An IEnumerable&lt;ContestIvrResultModel&gt;
        /// </returns>
        public ContestIvrResultModel GetContestsForIvrSystem(string electionId)
        {
            var intElectionId = Convert.ToInt32(electionId);

            var retrievedContests = _contestsRepository.GetContestsByElectionId(intElectionId);

            // Must map the collection of Contest to a List<ContestIvrResultModel>
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contest, ContestResultModel>()
                    .ForMember(
                        dest => dest.ContestID,
                        opt => opt.MapFrom(src => src.Id));
            });

            var iMapper = mapperConfig.CreateMapper();
            var contestResults =
                iMapper.Map<IEnumerable<Contest>, IEnumerable<ContestResultModel>>(retrievedContests);

            var results = new ContestIvrResultModel {Contests = contestResults.ToList()};

            return results;
        }

    }
}
