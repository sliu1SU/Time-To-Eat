using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Home
{
    /// <summary>
    /// Unit Testing class for home page
    /// - Testing Methods:
    ///     - OnGet()
    /// </summary>
    public class HomeTests
    {
        #region TestSetup

        /// Home model page instace to test 
        public static HomeModel pageModel;

        /// <summary>
        /// Setting up the environment/model
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new HomeModel()
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// ModelState.IsValid should return true after OnGet is called 
        /// with valid home page
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Home_Page()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}