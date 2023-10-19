using ContosoCrafts.WebSite.Pages.Restaurants;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Create
{
    /// <summary>
    /// Unit tests for the UpdateModel class 
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup

        // Update Model Page instance to test
        public static UpdateModel pageModel;

        /// <summary>
        /// SetUp UpdateModel for the tests 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Page model 
            pageModel = new UpdateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the Valid OnGet method with product id 
        /// </summary>
        [Test]
        public void OnGet_Valid_With_Non_Existing_ProductID_Should_Return_RedirectToPage_Index()
        {
            // Arrange
            var expected = true;

            // Act
            var result = pageModel.OnGet("not-existing-product-id") as RedirectToPageResult;

            var actual = result.PageName.Contains("Index");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Valid OnGet method with product id 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("German");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Rhein Haus", pageModel.Product.Title);
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Tests OnPost method returns to detail page with valid data 
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Redirect_To_Detail_Page()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First();
            pageModel.Product = data;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Detail"));
        }

        /// <summary>
        /// Tests OnPost method returns to update page with invalid data 
        /// </summary>
        [Test]
        public void OnPost_Invalid_Should_Return_Same_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel();

            // Force an invalid error state
            pageModel.ModelState.AddModelError("title-err", "title required");

            // Act
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests OnPost method redirects to index page with invalid data
        /// </summary>
        [Test]
        public void OnPost_Invalid_Should_Return_Redirect_Index_Page()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First();
            data.Id = "not-found-id";
            pageModel.Product = data;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        #endregion OnPost
    }
}