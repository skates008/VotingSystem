using System;
using System.Linq;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public class ElectionsServices : IElectionsServices
    {
        private readonly IElectionsRepository _electionsRepository;
        private readonly IWebConfigReaderService _webConfigReaderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionsServices"/> class.
        /// </summary>
        /// <param name="webConfigReaderService">
        /// NOT CURRENTLY USED, but may use this 'slot' in the future. -SKF
        /// </param>
        /// <param name="electionsRepository">
        /// The elections repository.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="electionsRepository"/> is null.
        /// </exception>
        public ElectionsServices(
            IWebConfigReaderService webConfigReaderService,
            IElectionsRepository electionsRepository)
        {
            _webConfigReaderService = webConfigReaderService;
            _electionsRepository = electionsRepository ?? throw new ArgumentNullException(nameof(electionsRepository));
        }

        /// <summary>
        /// Gets the election identifier open and closed.
        /// </summary>
        /// <param name="callingAddress">The calling address.</param>
        /// <returns>
        /// An instance of the <see cref="ElectionIdOpenAndClosed"/> class, or
        /// null if the election is not found.
        /// </returns>
        public ElectionIdOpenAndClosed GetElectionIdOpenAndClosed(
            string callingAddress)
        {
            // get the record, then do the mapping
            var electionInfo = _electionsRepository.Get(q => q.IvrPhoneNumber.Equals(callingAddress));

            if (electionInfo == null)
            {
                var notFoundElectionInfo = new ElectionIdOpenAndClosed
                {
                    Election_ID = null, Open_DateTime = null, Close_DateTime = null
                };

                return notFoundElectionInfo;
            }

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Election, ElectionIdOpenAndClosed>()
                    .ForMember(
                        dest => dest.Election_ID,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(
                        dest => dest.Open_DateTime,
                        opt => opt.ConvertUsing(
                            new DateToStringConverterForAm(),
                            src => src.OpenDate))
                    .ForMember(
                        dest => dest.Close_DateTime,
                        opt => opt.ConvertUsing(
                            new DateToStringConverterForAm(),
                            src => src.CloseDate));
            });

            // map it to a ElectionIdOpenAndClosed
            var iMapper = mapperConfig.CreateMapper();
            var electionIdOpenAndClosed = iMapper.Map<Election, ElectionIdOpenAndClosed>(electionInfo);

            return electionIdOpenAndClosed;
        }

    }

    public class DateToStringConverterForAm : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime dateTimeSource, ResolutionContext context)
        {
            return string.Concat(dateTimeSource.ToString("s"), "Z");
        }
    }


}
