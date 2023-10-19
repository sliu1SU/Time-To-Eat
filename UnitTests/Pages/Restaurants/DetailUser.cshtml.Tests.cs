using System.Linq;
using ContosoCrafts.WebSite.Pages.Restaurants;
using NUnit.Framework;

namespace UnitTests.Pages.Restaurants
{
    /// <summary>
    /// Unit tests for the DetailUserModel class.
    /// </summary>
    internal class DetailUserTests
    {
        #region TestSetup

        // Read Model Page instance to test
        public static DetailUserModel pageModel;

        /// <summary>
        /// SetUp ReadModel for the tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Page model 
            pageModel = new DetailUserModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test the Valid OnGet Http request method with random product id
        /// </summary>
        [Test]
        public void OnGet_Valid_With_New_Product_ID_Should_Retrieve_Null()
        {
            // Arrange
            var Id = System.Guid.NewGuid().ToString();

            // Act
            pageModel.OnGet(Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Product);
        }

        /// <summary>
        /// Test the OnGet Http request method with existing product id
        /// </summary>
        [Test]
        public void OnGet_Valid_With_Existing_Product_ID_Should_Retrieve_Expected_Product()
        {
            // Arrange
            var Product = TestHelper.ProductService.GetProducts().First();

            // Act
            pageModel.OnGet(Product.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(Product.Id, pageModel.Product.Id);
        }

        /// <summary>
        /// Test the OnGet function with user comment
        /// </summary>
        [Test]
        public void OnGet_Valid_Comment_Should_Add_Comment()
        {
            // Arrange
            var comment = "testing";

            var data = TestHelper.ProductService.GetAllData().First();

            pageModel.Comment = comment;

            // Act
            pageModel.OnGet(data.Id);
            var result = TestHelper.ProductService.GetProduct(data.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(comment, result.CommentList.Last().Comment);
        }

        #endregion OnGet
    }
}