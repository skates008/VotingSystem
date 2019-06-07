using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using VotingSite.Controllers;
using VotingSite.DataAccessServices;
using VotingSite.DAL;
using VotingSite.Services;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;


namespace VotingSite.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task WhenSiteFirstNavigatedTo_IndexMethod_ShouldGetTheLoginScreenData()
        {
            // Arrange
            const int expectedElectionId = 1;

            var mockWebConfigReaderService = new Mock<IWebConfigReaderService>();
            mockWebConfigReaderService.Setup(x => x.GetAppSetting<int>("CurrentElectionId"))
                .Returns(expectedElectionId);

            var uiDepLoginSvcs = new Mock<IUiDependentLoginServices>();
            uiDepLoginSvcs.Setup(o => o.GetLoginScreenDataAsync(It.IsAny<int>()))
                .Returns<int>(x =>
                    Task.FromResult(new LoginViewModel
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
                        UserIp = "",
                        BrowserAgent = ""
                    }));

            // ReSharper disable once IdentifierTypo
            //var uiDepLoginSvcs = new Mock<IUiDependentLoginServices>();
            var loginServicesMock = new Mock<LoginServices>();

            var mockUserCredentialsModel = new Mock<IUserCredentialsValidation>();

            var request = new Mock<HttpRequestBase>();
            request.Setup(p => p.UserAgent).Returns("userAgent");
            request.Setup(p => p.UserHostAddress).Returns("1.2.3.4");
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Request).Returns(request.Object);

            var controller = new HomeController(
                mockWebConfigReaderService.Object,
                loginServicesMock.Object,
                uiDepLoginSvcs.Object,
                mockUserCredentialsModel.Object);

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            var loginVm = result?.Model as LoginViewModel;

            Assert.IsNotNull(result);
            Assert.IsNotNull(loginVm);
            uiDepLoginSvcs.Verify(uut => uut.GetLoginScreenDataAsync(expectedElectionId), Times.Once);

            Assert.AreEqual("1.2.3.4", loginVm.UserIp);
            Assert.AreEqual("userAgent", loginVm.BrowserAgent);
            Assert.AreEqual(expectedElectionId, loginVm.ElectionId);
        }

    }
}
