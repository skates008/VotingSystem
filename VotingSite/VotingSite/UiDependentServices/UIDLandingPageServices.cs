using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using VotingSite.DAL;
using VotingSite.Domain;
using VotingSite.UiDependentModels;

namespace VotingSite.UiDependentServices
{
    // IUIDLandingPageServices 
    public class UIDLandingPageServices : IUIDLandingPageServices
    {
        private readonly ILandingPageDataAccess _landingPageDataAccess;

        public UIDLandingPageServices(
            ILandingPageDataAccess landingPageDataAccess)
        {
            _landingPageDataAccess = landingPageDataAccess ??
                                     throw new ArgumentNullException(nameof(landingPageDataAccess));
        }


        public async Task<LandingPgViewModel> GetLandingPageDataAsync(int electionId)
        {
            // CALLING: async Task<LandingPageViewData> GetLandingPageViewData(int electionId)
            LandingPageViewData retrievedLandingPgData = await _landingPageDataAccess.GetLandingPageViewData(electionId);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LandingPageViewData, LandingPgViewModel>();

                cfg.CreateMap<List<ContestDto>, LandingPgViewModel>()
                    .ForMember(dest => dest.BallotData.Contests,
                        opt => opt.MapFrom(
                            src => Mapper.Map<List<ContestDto>, List<ContestDto>>(src)));
            });
            var iMapper = mapperConfig.CreateMapper();
            var landingPgViewModel = iMapper.Map<LandingPageViewData, LandingPgViewModel>(retrievedLandingPgData);

            // build the 'html Id' value 
            var tempId = 1;
            var htmlContestId = $"ContestItem_{tempId}_Id";
            foreach (var contest in landingPgViewModel.BallotData.Contests)
            {
                contest.HtmlContestId = htmlContestId;
                tempId++;
            }

            return landingPgViewModel;
        }


    }
}