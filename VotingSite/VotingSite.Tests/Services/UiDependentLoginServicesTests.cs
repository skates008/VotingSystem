using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using VotingSite.DAL;
using VotingSite.DataAccessServices;
using VotingSite.Domain;
using VotingSite.Services;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;


namespace VotingSite.Tests.Services
{
    /// <summary>
    /// Summary description for LoginServicesTests
    /// </summary>
    [TestClass]
    public class UiDependentLoginServicesTests
    {
        [TestMethod]
        public async Task GivenValidInput_GetLoginScreenDataAsync_ShouldReturnAHydratedLoginViewModel()
        {
            // Arrange
            const int expectedElectionId = 1;

            var mockWebConfigReaderService = new Mock<IWebConfigReaderService>();
            mockWebConfigReaderService.Setup(x => x.GetAppSetting<int>("CurrentElectionId"))
                .Returns(expectedElectionId);

            // gotta mock ILoginScreenDataAccess
            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();
            // async Task<LoginViewModel> GetDataForLoginScreenAsync(int electionId)
            mockLoginScreenDataAccess.Setup(mut => mut.GetDataForLoginScreenAsync(expectedElectionId))
                .Returns<int>(x =>
                    Task.FromResult(new LoginViewData
                    {
                        // ElectionLogoUrl = "",
                        ElectionId = x,
                        ElectionName = "2019 Member-at-Large Board Election",
                        LoginScreenOpenMessage =
                            "To begin the voting process, locate your Personal Identification Number (PIN) on your ballot package received by mail. See example (<a href=\"http://www.pdf995.com/samples/pdf.pdf\" target=\"_blank\" >PDF</a>)<BR /><BR />Call the Everyone Counts/IVS Customer Service Line at (888) 492-4763 if you need assistance locating your PIN.",
                        LoginScreenCloseMessage = "Sorry dude, voting is uh, Cah-lozed.",
                        LoginIdLabelTxt = "PIN (Required)",
                        LoginPinLabelTxt = "Last 4 digits of Social Security number (required)",
                        OpenDate = new DateTime(2019, 5, 2, 0, 0, 0),
                        CloseDate = new DateTime(2019, 5, 3, 11, 59, 59),
                        LandingPageTitle = "Success!",
                        LandingPageMessage =
                            "By submitting my vote online, I HEREBY CERTIFY UNDER PENALTY OF PERJURY that I was... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec at euismod erat, vitae iaculis lectus. Aliquam sagittis a eros sed dignissim. Duis aliquet convallis purus, eget elementum tellus consequat in. Suspendisse quis sollicitudin nisl. Quisque ultricies, eros ut laoreet vehicula, mi tellus condimentum est, fermentum luctus enim justo a arcu. Ut euismod leo libero. Aenean lobortis auctor urna vel luctus. Suspendisse sit amet turpis mattis, tristique urna ac, mattis nunc. Aenean a purus feugiat, aliquet ligula vitae, semper felis. Sed gravida sodales laoreet. Nullam ullamcorper congue erat tristique maximus. Suspendisse quis odio sollicitudin magna pretium ultrices at vel ligula.",
                        VotingIsOpen = false,
                        UserIp = "usersIpAddress",
                        BrowserAgent = "usersHost"
                    }));

            var service = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);
            var expectedReturnedType = typeof(LoginViewModel);

            // Act
            var callResult = await service.GetLoginScreenDataAsync(expectedElectionId);

            // Assert
            mockLoginScreenDataAccess.Verify(
                mut => mut.GetDataForLoginScreenAsync(expectedElectionId),
                Times.Once);
            Assert.IsNotNull(callResult);
            Assert.IsInstanceOfType(callResult, expectedReturnedType);

            Assert.AreEqual(expectedElectionId, callResult.ElectionId);
            Assert.AreEqual("Success!", callResult.LandingPageTitle);
        }

        [TestMethod]
        public async Task IfNowIsOutsideElectionsOpenClosedDataRange_VotingIsOpenShouldBeReturnedAsFalse_FromGetLoginScreenDataAsync()
        {
            // Arrange
            const int expectedElectionId = 1;

            var mockWebConfigReaderService = new Mock<IWebConfigReaderService>();
            mockWebConfigReaderService.Setup(x => x.GetAppSetting<int>("CurrentElectionId"))
                .Returns(expectedElectionId);

            var openDt = DateTime.Now.AddDays(-5);
            var closedDt = DateTime.Now.AddDays(-3);

            // gotta mock ILoginScreenDataAccess
            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();
            // async Task<LoginViewModel> GetDataForLoginScreenAsync(int electionId)
            mockLoginScreenDataAccess.Setup(mut => mut.GetDataForLoginScreenAsync(expectedElectionId))
                .Returns<int>(x =>
                    Task.FromResult(new LoginViewData
                    {
                        // ElectionLogoUrl = "",
                        ElectionId = x,
                        ElectionName = "2019 Member-at-Large Board Election",
                        LoginScreenOpenMessage =
                            "To begin the voting process, locate your Personal Identification Number (PIN) on your ballot package received by mail. See example (<a href=\"http://www.pdf995.com/samples/pdf.pdf\" target=\"_blank\" >PDF</a>)<BR /><BR />Call the Everyone Counts/IVS Customer Service Line at (888) 492-4763 if you need assistance locating your PIN.",
                        LoginScreenCloseMessage = "Sorry dude, voting is uh, Cah-lozed.",
                        LoginIdLabelTxt = "PIN (Required)",
                        LoginPinLabelTxt = "Last 4 digits of Social Security number (required)",
                        OpenDate = openDt,
                        CloseDate = closedDt,
                        LandingPageTitle = "Success!",
                        LandingPageMessage =
                            "By submitting my vote online, I HEREBY CERTIFY UNDER PENALTY OF PERJURY that I was... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec at euismod erat, vitae iaculis lectus. Aliquam sagittis a eros sed dignissim. Duis aliquet convallis purus, eget elementum tellus consequat in. Suspendisse quis sollicitudin nisl. Quisque ultricies, eros ut laoreet vehicula, mi tellus condimentum est, fermentum luctus enim justo a arcu. Ut euismod leo libero. Aenean lobortis auctor urna vel luctus. Suspendisse sit amet turpis mattis, tristique urna ac, mattis nunc. Aenean a purus feugiat, aliquet ligula vitae, semper felis. Sed gravida sodales laoreet. Nullam ullamcorper congue erat tristique maximus. Suspendisse quis odio sollicitudin magna pretium ultrices at vel ligula.",
                        VotingIsOpen = false,
                        UserIp = "usersIpAddress",
                        BrowserAgent = "usersHost"
                    }));

            var uiDependentLoginServices = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);
            var expectedReturnedType = typeof(LoginViewModel);

            // Act
            var callResult = await uiDependentLoginServices.GetLoginScreenDataAsync(expectedElectionId);

            // Assert
            mockLoginScreenDataAccess.Verify(
                mut => mut.GetDataForLoginScreenAsync(expectedElectionId),
                Times.Once);
            Assert.IsNotNull(callResult);
            Assert.IsInstanceOfType(callResult, expectedReturnedType);

            Assert.IsFalse(callResult.VotingIsOpen);
        }

        [TestMethod]
        public async Task
            IfNowIsWithinElectionsOpenClosedDataRange_VotingIsOpenShouldBeReturnedAsTrueFrom_GetLoginScreenDataAsync()
        {
            // Arrange
            const int expectedElectionId = 1;

            var mockWebConfigReaderService = new Mock<IWebConfigReaderService>();
            mockWebConfigReaderService.Setup(x => x.GetAppSetting<int>("CurrentElectionId"))
                .Returns(expectedElectionId);

            var openDt = DateTime.Now.AddDays(-1);
            var closedDt = DateTime.Now.AddDays(1);

            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();
            // async Task<LoginViewModel> GetDataForLoginScreenAsync(int electionId)
            mockLoginScreenDataAccess.Setup(mut => mut.GetDataForLoginScreenAsync(expectedElectionId))
                .Returns<int>(x =>
                    Task.FromResult(new LoginViewData
                    {
                        // ElectionLogoUrl = "",
                        ElectionId = x,
                        ElectionName = "2019 Member-at-Large Board Election",
                        LoginScreenOpenMessage =
                            "To begin the voting process, locate your Personal Identification Number (PIN) on your ballot package received by mail. See example (<a href=\"http://www.pdf995.com/samples/pdf.pdf\" target=\"_blank\" >PDF</a>)<BR /><BR />Call the Everyone Counts/IVS Customer Service Line at (888) 492-4763 if you need assistance locating your PIN.",
                        LoginScreenCloseMessage = "Sorry dude, voting is uh, Cah-lozed.",
                        LoginIdLabelTxt = "PIN (Required)",
                        LoginPinLabelTxt = "Last 4 digits of Social Security number (required)",
                        OpenDate = openDt,
                        CloseDate = closedDt,
                        LandingPageTitle = "Success!",
                        LandingPageMessage =
                            "By submitting my vote online, I HEREBY CERTIFY UNDER PENALTY OF PERJURY that I was... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec at euismod erat, vitae iaculis lectus. Aliquam sagittis a eros sed dignissim. Duis aliquet convallis purus, eget elementum tellus consequat in. Suspendisse quis sollicitudin nisl. Quisque ultricies, eros ut laoreet vehicula, mi tellus condimentum est, fermentum luctus enim justo a arcu. Ut euismod leo libero. Aenean lobortis auctor urna vel luctus. Suspendisse sit amet turpis mattis, tristique urna ac, mattis nunc. Aenean a purus feugiat, aliquet ligula vitae, semper felis. Sed gravida sodales laoreet. Nullam ullamcorper congue erat tristique maximus. Suspendisse quis odio sollicitudin magna pretium ultrices at vel ligula.",
                        VotingIsOpen = false,
                        UserIp = "usersIpAddress",
                        BrowserAgent = "usersHost"
                    }));

            var service = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);
            var expectedReturnedType = typeof(LoginViewModel);

            // Act
            var callResult = await service.GetLoginScreenDataAsync(expectedElectionId);

            // Assert
            mockLoginScreenDataAccess.Verify(
                mut => mut.GetDataForLoginScreenAsync(expectedElectionId),
                Times.Once);
            Assert.IsNotNull(callResult);
            Assert.IsInstanceOfType(callResult, expectedReturnedType);

            Assert.IsTrue(callResult.VotingIsOpen);
        }


        [TestMethod]
        public void TestBuildingOfTheLandingPageViewModel_ShouldReturnAHydratedInstance()
        {
            // Arrange

            // shouldn't actually be called, so no Setup necessary
            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();

            var uiDependentLoginServices = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);

            var loginViewModel = new LoginViewModel
            {
                ElectionId = 37,
                LandingPageTitle = "Lpt",
                LandingPageMessage = "landingPageMessage"
            };

            // Act
            var uutResult = uiDependentLoginServices.BuildLandingPgViewModel(loginViewModel);

            // Assert
            Assert.IsInstanceOfType(uutResult, typeof(LandingPgViewModel));

            Assert.AreEqual(37, uutResult.ElectionId);
            Assert.AreEqual("Lpt", uutResult.LandingPageTitle);
            Assert.AreEqual("landingPageMessage", uutResult.LandingPageMessage);
        }

        [TestMethod]
        public void VotingIsOpenVerification_WithAppropriateSettings_ShouldReturnWithVotingIsOpenAsTrue()
        {
            // shouldn't actually be called, so no Setup necessary
            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();

            var uiDependentLoginServices = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);

            var now = new DateTime(2019, 5, 5, 14, 45, 00);

            var loginViewModel = new LoginViewModel
            {
                ElectionId = 37,
                OpenDate = new DateTime(2019,5,5, 14,30, 00),
                CloseDate = new DateTime(2019, 5, 5, 15,00, 00)
            };

            // Act
            var uutResult = uiDependentLoginServices.VotingIsOpenVerification(loginViewModel, now);

            Assert.IsTrue(uutResult.VotingIsOpen);
        }

        [TestMethod]
        public void VotingIsOpenVerification_WithAppropriateSettings_ShouldReturnWithVotingIsOpenAsFalse()
        {
            // shouldn't actually be called, so no Setup necessary
            var mockLoginScreenDataAccess = new Mock<ILoginScreenDataAccess>();

            var uiDependentLoginServices = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);

            var now = new DateTime(2019, 5, 5, 14, 29, 59);

            var loginViewModel = new LoginViewModel
            {
                ElectionId = 37,
                OpenDate = new DateTime(2019,5,5, 14,30, 00),
                CloseDate = new DateTime(2019, 5, 5, 15,00, 00)
            };

            // Act
            var uutResult = uiDependentLoginServices.VotingIsOpenVerification(loginViewModel, now);

            Assert.IsFalse(uutResult.VotingIsOpen);
        }



    }
}
