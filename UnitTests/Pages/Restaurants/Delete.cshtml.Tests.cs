using ContosoCrafts.WebSite.Pages.Restaurants;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Restaurants
{
    /// <summary>
    /// Unit tests for the Delete Model
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        
        // The delete page model to test
        public static DeleteModel pageModel;

        /// <summary>
        /// Setup the test environment for the delete page model
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.ProductService);
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test the OnGet method with a valid id remove product from database
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Remove_Product_Return_Redirect_To_Index()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().Last();

            var expected = data.Id;

            // Act
            pageModel.OnGet(data.Id);

            var actual = pageModel.Product.Id;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(expected, actual);
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test remove product successfully from database
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Remove_Product_From_Data_Storage()
        {
            // Arrange
            pageModel.Product = TestHelper.ProductService.GetProducts().Last();
            var expected = TestHelper.ProductService.GetProducts().Count() - 1;

            // Act
            pageModel.OnPost();
            var actual = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // number of products in database should be one less than before
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test redirect to index page after delete
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Redirect_To_Index_Page()
        {
            // Arrange
            pageModel.Product = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // redirect to index page
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Test invalid pageModel condition
        /// </summary>
        [Test]
        public void OnPost_Invalid_Should_Return_Same_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel();
            // force an invalid error state
            pageModel.ModelState.AddModelError("delete-err", "under-cache");

            // Act
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        #endregion OnPost
    }
}