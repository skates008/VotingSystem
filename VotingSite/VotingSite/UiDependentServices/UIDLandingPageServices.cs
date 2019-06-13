using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VotingSite.DAL;
using VotingSite.Domain;

namespace VotingSite.UiDependentServices
{
    // IUIDLandingPageServices 
    public class UIDLandingPageServices
    {
        private readonly ILandingPageDataAccess _landingPageDataAccess;

        public UIDLandingPageServices(
            ILandingPageDataAccess landingPageDataAccess)
        {
            _landingPageDataAccess = landingPageDataAccess ??
                                     throw new ArgumentNullException(nameof(landingPageDataAccess));
        }


        public async Task<LandingPageViewData> GetLandingPageDataAsync(int electionId)
        {
            //LoginViewData retrievedLoginData = await _loginScreenDataAccess.GetDataForLoginScreenAsync(electionId);

            //var dtNow = DateTime.Now;
            //if (dtNow >= retrievedLoginData.OpenDate && dtNow <= retrievedLoginData.CloseDate)
            //{
            //    retrievedLoginData.VotingIsOpen = true;
            //}

            //// Map LoginViewData -> LoginViewModel
            //var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<LoginViewData, LoginViewModel>(); });
            //var iMapper = mapperConfig.CreateMapper();
            //var loginViewModel = iMapper.Map<LoginViewData, LoginViewModel>(retrievedLoginData);

            //return loginViewModel;

            return await Task.FromResult(new LandingPageViewData());
        }


    }
}