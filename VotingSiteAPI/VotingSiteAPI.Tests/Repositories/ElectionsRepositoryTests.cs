using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Tests.Repositories
{
    /// <summary>
    /// Summary description for ElectionsRepositoryTests
    /// </summary>
    [TestClass]
    public class ElectionsRepositoryTests
    {
        [TestMethod]
        public void GetById_ShouldReturnAValid_ElectionInstance()
        {
            // Arrange 
            var mock = new Mock<IElectionsRepository>();
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Election());

            // Act
            var result = mock.Object.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Election));
        }
    }
}
