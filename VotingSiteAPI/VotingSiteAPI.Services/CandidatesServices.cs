using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public class CandidatesServices : ICandidatesServices
    {
        private readonly ICandidatesRepository _candidatesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatesServices"/> class.
        /// </summary>
        /// <param name="candidatesRepository">The candidates repository.</param>
        /// <exception cref="ArgumentNullException">candidatesRepository</exception>
        public CandidatesServices(
            ICandidatesRepository candidatesRepository)
        {
            _candidatesRepository =
                candidatesRepository ?? throw new ArgumentNullException(nameof(candidatesRepository));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the candidates by contest identifier.
        /// </summary>
        /// <param name="contestId">
        /// An integer containing the contest identifier.
        /// </param>
        /// <returns>
        /// IEnumerable&lt;Candidate&gt;
        /// </returns>
        public IEnumerable<Candidate> GetCandidatesByContestId(int contestId)
        {
            var results = _candidatesRepository
                .GetMany(can => can.ContestId == contestId)
                .OrderBy(can => can.SortOrder);

            return results;
        }


        /// <summary>
        /// Gets the candidates for ivr system.
        /// </summary>
        /// <param name="contestIdString">
        /// The contest identifier as a string.
        /// </param>
        /// <remarks>
        /// for: IHttpActionResult VotingSiteAPI.Controllers.CandidatesIvrController(string contestId)
        /// </remarks>
        /// <returns>
        /// A hydrated instance of the <see cref="CandidateIvrResultModel"/>
        /// class.
        /// </returns>
        public CandidateIvrResultModel GetCandidatesForIvrSystem(string contestIdString)
        {
            var contestId = Convert.ToInt32(contestIdString);

            var candidates = _candidatesRepository
                .GetMany(can => can.ContestId == contestId)
                .OrderBy(can => can.SortOrder);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Candidate, CandidateIvrReturnModel>()
                    .ForMember(
                        dest => dest.CandidateID,
                        opt => opt.MapFrom(src => src.Id));
            });

            // map it to a ElectionIdOpenAndClosed
            var iMapper = mapperConfig.CreateMapper();
            var resultModels =
                iMapper.Map<IEnumerable<Candidate>, IEnumerable<CandidateIvrReturnModel>>(candidates);

            var result = new CandidateIvrResultModel {Candidates = resultModels};

            return result;
        }

    }
}
