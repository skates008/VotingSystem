using System;
using System.Web.Http;
using System.Web.Http.Results;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using VotingSiteAPI.Controllers;
using VotingSiteAPI.Services;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        /* [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }  */

        // TODO: Fix these tests; LoginController() now requires an instance of the WebConfigContainer class.

        ////[TestMethod]
        ////public void UserCredentialsAreValid_ShouldReturn_Bool()
        ////{
        ////    // Arrange 
        ////    var mockLoginServices = new Mock<ILoginServices>();
        ////    var uc = new UserCredentialsModel
        ////    {
        ////        UsernameOrId = "22222342",
        ////        PasswordOrPin = "1234"
        ////    };

        ////    // Act
        ////    var controller = new LoginController(mockLoginServices.Object);

        ////    var result = controller.LoginWebsiteUser(uc);

        ////    var resultData = ((OkNegotiatedContentResult<bool>) result).Content;

        ////    // Assert
        ////    mockLoginServices.Verify(ut => ut.ValidateUserCredentials(uc), Times.Once);
        ////    Assert.IsInstanceOfType(resultData, typeof(bool));
        ////}

        ////[TestMethod]
        ////public void UserCredentialsAreValid_ShouldReturn_False()
        ////{
        ////    // Arrange 
        ////    var mockLoginServices = new Mock<ILoginServices>();
        ////    var uc = new UserCredentialsModel
        ////    {
        ////        UsernameOrId = "22222342",
        ////        PasswordOrPin = "1234"
        ////    };

        ////    // Act
        ////    var controller = new LoginController(mockLoginServices.Object);

        ////    var result = controller.LoginWebsiteUser(uc);

        ////    var resultData = ((OkNegotiatedContentResult<bool>)result).Content;

        ////    // Assert
        ////    mockLoginServices.Verify(ut => ut.ValidateUserCredentials(uc), Times.Once);
        ////    Assert.IsInstanceOfType(resultData, typeof(bool));
        ////    Assert.IsFalse(resultData, "For this case, the result should be false!");
        ////}

        ////[TestMethod]
        ////public void UserCredentialsAreValid_ShouldReturn_True()
        ////{
        ////    // Arrange 
        ////    var mockLoginServices = new Mock<ILoginServices>();
        ////    var uc = new UserCredentialsModel
        ////    {
        ////        UsernameOrId = "22222222",
        ////        PasswordOrPin = "1234"
        ////    };

        ////    // Act
        ////    var controller = new LoginController(mockLoginServices.Object);

        ////    var result = controller.LoginWebsiteUser(uc);

        ////    var resultData = ((OkNegotiatedContentResult<bool>)result).Content;

        ////    // Assert
        ////    mockLoginServices.Verify(ut => ut.ValidateUserCredentials(uc), Times.Once);
        ////    Assert.IsInstanceOfType(resultData, typeof(bool));
        ////    Assert.IsFalse(resultData, "For this case, the result should be false!");
        ////}


        ////[TestMethod]
        ////public void GetPageDetails_ShouldReturnHydratedModel()
        ////{
        ////    // Arrange 
        ////    var expectedOpenDate = DateTime.MinValue;
        ////    var expectedCloseDate = DateTime.MaxValue;
        ////    const string expectedLoginScreenOpenMessage = "LoginScreenOpenMessage";
        ////    const string expectedLoginScreenCloseMessage = "LoginScreenCloseMessage007";
        ////    const string expectedLoginIdLabelTxt = "LoginIdLabelTxtXX";
        ////    const string expectedLoginPinLabelTxt = "LoginPinLabelTxt";
        ////    const string expectedLandingPageTitle = "LandingPageTitle";
        ////    const string expectedLandingPageMessage = "LandingPageMessage";

        ////    var mockLoginServices = new Mock<ILoginServices>();
        ////    mockLoginServices.Setup(uut => uut.GetPreLoginElectionData(It.IsAny<int>()))
        ////        .Returns(
        ////            new RetrievedPageDataModel
        ////            {
        ////                OpenDate = expectedOpenDate,
        ////                CloseDate = expectedCloseDate,
        ////                LoginScreenOpenMessage = expectedLoginScreenOpenMessage,
        ////                LoginScreenCloseMessage = expectedLoginScreenCloseMessage,
        ////                LoginIdLabelTxt = expectedLoginIdLabelTxt,
        ////                LoginPinLabelTxt = expectedLoginPinLabelTxt,
        ////                LandingPageTitle = expectedLandingPageTitle,
        ////                LandingPageMessage = expectedLandingPageMessage
        ////            });

        ////    var controller = new LoginController(mockLoginServices.Object);

        ////    // Act
        ////    var result = controller.GetPageData(1);

        ////    var resultData =
        ////        (OkNegotiatedContentResult<RetrievedPageDataModel>) result;

        ////    // Assert
        ////    Assert.IsNotNull(result);
        ////    Assert.IsInstanceOfType(result, typeof(IHttpActionResult));

        ////    // testing field contents is fairly useless when all fields are nullable.
        ////    // thus, just testing the type.
        ////    Assert.IsInstanceOfType(resultData, typeof(OkNegotiatedContentResult<RetrievedPageDataModel>));
        ////}
    }
}
