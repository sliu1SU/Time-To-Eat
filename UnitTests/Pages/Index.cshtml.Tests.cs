using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Index
{
    /// <summary>
    /// Unit tests for IndexModel class
    /// </summary>
    public class IndexTests
    {
        #region TestSetup

        // Index model page instance to test 
        public static IndexModel pageModel;

        /// <summary>
        /// SetUp IndexModel for unit tests 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // MockLoggerDirect 
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            // Page model 
            pageModel = new IndexModel(MockLoggerDirect, TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests OnGet method returns products 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }

        #endregion OnGet
    }
}