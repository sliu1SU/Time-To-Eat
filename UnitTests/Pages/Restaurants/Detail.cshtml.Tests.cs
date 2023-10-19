using System.Collections.Generic;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages.Restaurants;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Pages.Restaurants
{
    /// <summary>
    /// Unit tests for the ReadModel class.
    /// </summary>
    public class DetailTests
    {
        #region TestSetup

        // Read Model Page instance to test
        public static ReadModel pageModel;

        /// <summary>
        /// SetUp ReadModel for the tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Page model 
            pageModel = new ReadModel(TestHelper.ProductService)
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
        /// Test the OnGet Http request method product has null hours
        /// </summary>
        [Test]
        public void OnGet_Valid_With_Product_Hours_Null_Should_Return_Same_Page()
        {
            // Arrange
            var product = TestHelper.ProductService.CreateProduct( new ProductModel()
                {
                    Title = "Test Product",
                    Hours = null
                }
            );
            
            // Act
            pageModel.OnGet(product.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Test the OnGet Http request method product has weekday null hour
        /// </summary>
        [Test]
        public void OnGet_Valid_With_Product_That_Has_Weekday_Hour_Should_Return_Same_Page()
        {
            // Arrange
            var product = TestHelper.ProductService.CreateProduct(new ProductModel()
                {
                    Title = "Test Product",
                    Hours = new List<int[]>() { null, null, null, null, null, null, null }
                }
            );
            
            // Act
            pageModel.OnGet(product.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        
        /// <summary>
        /// Test the Days attribute of the ReadModel class
        /// </summary>
        [Test]
        public void GetDate_Valid_Should_Return_List_Of_Days()
        {
            // Arrange
            var days = new List<string> { 
                "Monday", "Tuesday", "Wednesday", 
                "Thursday", "Friday", "Saturday", "Sunday" };
            var product = TestHelper.ProductService.GetProducts().First();

            // Act
            pageModel.OnGet(product.Id);
            var result = pageModel.Days;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            foreach(var day in days)
            {
                Assert.AreEqual(true, result.Contains(day));
            }
            
        }

        /// <summary>
        /// Test the OnGet Http request method with valid comment
        /// </summary>
        [Test]
        public void OnGet_Valid_Comment_Should_Add_Comment()
        {
            // Arrange
            var Comment = "Test Comment";
            var product = TestHelper.ProductService.GetProducts().First();
            pageModel.Comment = Comment;

            // Act
            pageModel.OnGet(product.Id);
            var result = TestHelper.ProductService.GetProduct(product.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(result.CommentList.Last().Comment, Comment);
        }

        #endregion OnGet
    }
}