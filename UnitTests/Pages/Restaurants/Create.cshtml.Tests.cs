using ContosoCrafts.WebSite.Pages.Restaurants;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests.Pages.Create
{
    /// <summary>
    /// Unit tests for CreateModel class 
    /// </summary>
    public class CreateTests
    {
        #region TestSetup

        // Create Model Page instance to test
        public static CreateModel pageModel;

        // default invalid url
        public static string garbageUrl = "https://garbage-image-test.jpg";

        // default valid url
        public static string validUrl = "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png";

        /// <summary>
        /// SetUp CreateModel for the tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Page model 
            pageModel = new CreateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests OnGet method creates an empty product model with valid input 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Create_Empty_Product_Model()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Product.Id);
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Tests OnPost method will return to create page with invalid input
        /// </summary>
        [Test]
        public void OnPost_Invalid_Input_Should_Return_Create_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel();
            pageModel.ModelState.AddModelError("forcing an error", "forcing an error");

            // Act
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(pageModel.ModelState.IsValid, false);
        }

        /// <summary>
        /// Tests OnPost method returns to create page when an image is garbage 
        /// </summary>
        [Test]
        public void OnPost_Invalid_Garbage_Image_Should_Return_Create_Page()
        {
            // Arrange
            var data = new ProductModel()
            {
                Title = "Enter Title",
                Description = "Enter Description",
                Url = validUrl,
                Image = garbageUrl,
            };
            pageModel.Product = data;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;
            
            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests OnPost method will return create page if URL is garbage 
        /// </summary>
        [Test]
        public void OnPost_Invalid_Garbage_Url_Should_Return_Create_Page()
        {
            // Arrange
            var data = new ProductModel()
            {
                Title = "Enter Title",
                Description = "Enter Description",
                Url = garbageUrl,
                Image = validUrl,
            };
            pageModel.Product = data;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests OnPost method will return detail all validation passed
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Create_Product_Return_Detail_Page()
        {
            // Arrange
            var data = new ProductModel()
            {
                Title = "Enter Title",
                Description = "Enter Description",
                Url = validUrl,
                Image = validUrl,
            };
            pageModel.Product = data;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;
            
            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnPost
    }
}