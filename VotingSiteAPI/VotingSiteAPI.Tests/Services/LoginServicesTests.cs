using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;

using Moq;

using Neleus.LambdaCompare;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Tests.Services
{
    [TestClass]
    public class LoginServicesTests
    {
        private static readonly DateTime ExpectedOpenDate = new DateTime(2019, 4, 24, 9, 50, 32);
        private static readonly DateTime ExpectedCloseDate = new DateTime(2019, 4, 27, 11, 59, 59);
        private const string ExpectedLoginScreenOpenMessage = "LoginScreenOpenMessage";
        private const string ExpectedLoginScreenCloseMessage = "LoginScreenCloseMessage007";
        private const string ExpectedLoginIdLabelTxt = "LoginIdLabelTxtXX";
        private const string ExpectedLoginPinLabelTxt = "LoginPinLabelTxt";
        private const string ExpectedLandingPageTitle = "LandingPageTitle";
        private const string ExpectedLandingPageMessage = "LandingPageMessage";


        [TestMethod]
        public void GetPreLoginElectionData_ShouldReturnAValid_RetrievedPageDataModel()
        {
            // Arrange 
            // because the LoginServices class requires an instance of the 
            // IElectionsRepository
            var mockElectionsRepo = new Mock<IElectionsRepository>();
            mockElectionsRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(electionId =>
                {
                    var newElection = new Election
                    {
                        Id = electionId,
                        OpenDate = ExpectedOpenDate,
                        CloseDate = ExpectedCloseDate,
                        LoginScreenOpenMessage = ExpectedLoginScreenOpenMessage,
                        LoginScreenCloseMessage = ExpectedLoginScreenCloseMessage,
                        LoginIdLabelTxt = ExpectedLoginIdLabelTxt,
                        LoginPinLabelTxt = ExpectedLoginPinLabelTxt,
                        LandingPageTitle = ExpectedLandingPageTitle,
                        LandingPageMessage = ExpectedLandingPageMessage
                    };
                    return newElection;
                });

            var mockVotesRepo = new Mock<IVotesRepository>();

            var mockVotersRepo = new Mock<IVotersRepository>();
            Expression<Func<Voter, bool>> testExpression = expr => (expr.LoginId == "22222222");
            mockVotersRepo.Setup(x => x.Get(It.Is<Expression<Func<Voter, bool>>>(
                    criteria => Lambda.Eq(criteria, testExpression))))
                .Returns<Voter>(voter =>
                {
                    var newVoter = new Voter
                    {
                        LoginId = voter.LoginId,
                        LoginPin = "1234"
                    };

                    return newVoter;
                });

            var mockLoginAttemptsRepo = new Mock<ILoginAttemptsRepository>();

            var loginServices = new LoginServices(
                mockElectionsRepo.Object,
                mockVotesRepo.Object,
                mockVotersRepo.Object,
                mockLoginAttemptsRepo.Object);

            // Act
            var result = loginServices.GetPreLoginElectionData(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RetrievedPageDataModel));
            Assert.AreEqual(ExpectedOpenDate , result.OpenDate);
            Assert.AreEqual(ExpectedCloseDate, result.CloseDate);
            Assert.AreEqual(ExpectedLoginScreenOpenMessage, result.LoginScreenOpenMessage);
            Assert.AreEqual(ExpectedLoginScreenCloseMessage, result.LoginScreenCloseMessage);
            Assert.AreEqual(ExpectedLoginIdLabelTxt, result.LoginIdLabelTxt);
            Assert.AreEqual(ExpectedLoginPinLabelTxt, result.LoginPinLabelTxt);
            Assert.AreEqual(ExpectedLandingPageTitle, result.LandingPageTitle);
            Assert.AreEqual(ExpectedLandingPageMessage, result.LandingPageMessage);
        }

        [TestMethod]
        public void ValidateUserCredentials_ShouldReturn_True()
        {
            // Arrange 
            var uc = new UserCredentialsModel
            {
                ElectionId = 1,
                UsernameOrId = "22222222",
                PasswordOrPin = "1234"
            };

            // Act
            var result = TestValidateUserCredentials(uc);

            // Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUserCredentials_ShouldReturn_False()
        {
            // Arrange 
            var uc = new UserCredentialsModel
            {
                ElectionId = 1,
                UsernameOrId = "22222342",
                PasswordOrPin = "1234"
            };

            // Act
            var result = TestValidateUserCredentials(uc);

            // Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Avoiding having all this code written twice just to change one
        /// line for the different tests
        /// </summary>
        /// <param name="ucm"></param>
        /// <returns></returns>
        private bool TestValidateUserCredentials(UserCredentialsModel ucm)
        {
            // Arrange 
            var mockElectionsRepo = new Mock<IElectionsRepository>();
            mockElectionsRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(electionId =>
                {
                    var newElection = new Election();   // this test doesn't care about this.
                    //{
                    //    Id = electionId,
                    //    OpenDate = ExpectedOpenDate,
                    //    CloseDate = ExpectedCloseDate,
                    //    LoginScreenOpenMessage = ExpectedLoginScreenOpenMessage,
                    //    LoginScreenCloseMessage = ExpectedLoginScreenCloseMessage,
                    //    LoginIdLabelTxt = ExpectedLoginIdLabelTxt,
                    //    LoginPinLabelTxt = ExpectedLoginPinLabelTxt,
                    //    LandingPageTitle = ExpectedLandingPageTitle,
                    //    LandingPageMessage = ExpectedLandingPageMessage
                    //};
                    return newElection;
                });

            var mockVotesRepo = new Mock<IVotesRepository>();

            var mockVotersRepo = new Mock<IVotersRepository>();
            //Expression<Func<Voter, bool>> testExpression = expr => (expr.LoginId == "22232222");
            //mockVotersRepo.Setup(x => x.Get(It.Is<Expression<Func<Voter, bool>>>(
            //        criteria => Lambda.Eq(criteria, testExpression)))) -- This is only needed if we're comparing the expressions themselves.

            var testDb = new List<Voter> { new Voter { ElectionId = 1, LoginId = "22222222", LoginPin = "1234" } };

            mockVotersRepo.Setup(_ => _.Get(It.IsAny<Expression<Func<Voter, bool>>>()))
                .Returns((Expression<Func<Voter, bool>> expr) =>
                {
                    var voter = testDb.FirstOrDefault(expr.Compile());

                    if (voter == null ||
                        (voter.LoginId != "22222222" || voter.LoginPin != "1234"))
                    {
                        return null;
                    }

                    return voter;
                });

            var mockLoginAttemptsRepo = new Mock<ILoginAttemptsRepository>();

            var loginServices = new LoginServices(
                mockElectionsRepo.Object,
                mockVotesRepo.Object,
                mockVotersRepo.Object,
                mockLoginAttemptsRepo.Object);

            return loginServices.ValidateUserCredentials(ucm);
        }

    }
}
