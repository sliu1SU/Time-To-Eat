using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework.Internal;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit tests for JsonFileProductService
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        /// <summary>
        /// Set up the test environment
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating

        /// <summary>
        /// Test AddRating with null product ID
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test AddRating with invalid product ID
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test AddRating with a rating of 5 and valid product ID
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Rating_5_Should_Return_True()
        {
            // Arrange
            var data = new ProductModel();

            var newData = TestHelper.ProductService.CreateProduct(data);

            // Act
            var result =  TestHelper.ProductService.AddRating(newData.Id, 5);

            var newProduct = TestHelper.ProductService.GetProduct(newData.Id);

            // Reset
            TestHelper.ProductService.DeleteData(newData.Id);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, newProduct.Ratings.Length);
            Assert.AreEqual(5, newProduct.Ratings.Last());
        }

        /// <summary>
        /// Test AddRating with invalid product ID
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_InvalidID_Should_Return_False()
        {
            // Arrange

            // Act
            bool result = TestHelper.ProductService.AddRating("sdasdasd", 2);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test AddRating with lower than 0 rating and valid product ID
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_Low_Should_Return_False()
        {
            // Arrange
            ProductModel data = TestHelper.ProductService.GetAllData().First();

            // Act
            bool result = TestHelper.ProductService.AddRating(data.Id, -8);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test AddRating with higher than 5 rating and valid product ID
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_High_Should_Return_False()
        {
            // Arrange
            ProductModel data = TestHelper.ProductService.GetAllData().First();

            // Act
            bool result = TestHelper.ProductService.AddRating(data.Id, 8);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test AddRating with empty rating and valid product ID
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Empty_Rating_Should_Return_True()
        {
            // Arrange

            var product = new ProductModel() { Ratings = new int[] {  } };

            var testId = TestHelper.ProductService.CreateProduct(product).Id;
            
            // Act
            // Add a rating for empty ratings test product
            bool result = TestHelper.ProductService.AddRating(testId, 5);

            // Get the test product
            var dataNewList = TestHelper.ProductService.GetProduct(testId);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }

        #endregion AddRating

        #region UpdateData

        /// <summary>
        /// Tests UpdateData with invalid product ID
        /// </summary>
        [Test]
        public void UpdateData_Invalid_ProductID_Should_Return_Null()
        {
            // Arrange
            ProductModel data = new ProductModel()
            {
                Id = "does not exist",
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
            };

            // Act
            ProductModel result = TestHelper.ProductService.UpdateData(data);

            // Assert
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Tests UpdateData with valid product ID
        /// </summary>
        [Test]
        public void UpdateData_Valid_ProductID_Should_Return_ProductModelObject()
        {
            // Arrange
            ProductModel data = TestHelper.ProductService.CreateProduct(new ProductModel()
            {
                Title = "test-Title",
                Description = "test-Description",
                Url = "test-URL",
                Image = "test-Image",
            });

            // remember old object state
            ProductModel oldState = TestHelper.ProductService.GetProduct(data.Id);
            oldState.Title = oldState.Title + "updated";
            oldState.Description = oldState.Description + "updated";
            oldState.Url = oldState.Url + "updated";
            oldState.Image = oldState.Image + "updated";

            // Act
            ProductModel result = TestHelper.ProductService.UpdateData(oldState);


            // Assert
            Assert.AreEqual(data.Title + "updated", result.Title);
            Assert.AreEqual(data.Description + "updated", result.Description);
            Assert.AreEqual(data.Url + "updated", result.Url);
            Assert.AreEqual(data.Image + "updated", result.Image);
        }

        #endregion UpdateData

        #region DeleteData

        /// <summary>
        /// Tests DeleteData with valid product ID
        /// </summary>
        [Test]
        public void DeleteData_Valid_ProductID_Should_Return_ProductModelObject()
        {
            // Arrange
            var oldState = TestHelper.ProductService.GetAllData().FirstOrDefault(x => x.Id.Equals("Steak House"));

            // Act
            var result = TestHelper.ProductService.DeleteData("Steak House");

            // Reset
            var newCreateData = TestHelper.ProductService.CreateData();

            var newData = TestHelper.ProductService.GetAllData().FirstOrDefault(x => x.Id.Equals(newCreateData.Id));
            newData = oldState;

            // Assert
            Assert.AreEqual(oldState.ToString(), result.ToString());
        }

        #endregion DeleteData

        #region GetProducts

        /// <summary>
        /// Tests GetProducts
        /// </summary>
        [Test]
        public void GetProducts_Valid_Should_Return_ListOfProduct()
        {
            // Arrange
            var listOfProducts = TestHelper.ProductService.GetProducts();

            var listOfProductsArray = listOfProducts.ToArray();

            // Act
            var result = TestHelper.ProductService.GetProducts();

            var resultArray = result.ToArray();

            // Assert
            for (int i = 0; i < listOfProductsArray.Length; i++)
            {
                Assert.AreEqual(listOfProductsArray[i].ToString(), resultArray[i].ToString());
            }
        }

        #endregion GetProducts

        #region GetProduct

        /// <summary>
        /// Tests GetProduct with invalid product ID
        /// </summary>
        [Test]
        public void GetProduct_Invalid_ProductID_Should_Return_Null()
        {
            // Arrange
            var ramdomId = System.Guid.NewGuid().ToString();

            // Act
            var result = TestHelper.ProductService.GetProduct(ramdomId);

            // Assert
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Tests GetProduct with valid product ID
        /// </summary>
        [Test]
        public void GetProduct_Valid_ProductID_Should_Return_ProductModelObject()
        {
            // Arrange
            var Id = TestHelper.ProductService.GetAllData().First().Id;

            // Act
            var result = TestHelper.ProductService.GetProduct(Id).Id;

            // Assert
            Assert.AreEqual(Id, result);
        }

        #endregion GetProduct

        #region CreateProduct

        /// <summary>
        /// Tests CreateProduct
        /// </summary>
        [Test]
        public void CreateProduct_Valid_Save_ProductModelObject_Should_Return_True()
        {
            // Arrange
            var data = new ProductModel();

            // Act
            var newData = TestHelper.ProductService.CreateProduct(data);

            var result = newData.Id != null;

            // Reset
            TestHelper.ProductService.DeleteData(newData.Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion CreateProduct

        #region AddComment

        /// <summary>
        /// Unit test for addComment with null product ID
        /// </summary>
        [Test]
        public void AddComment_Invalid_Null_ProductID_Should_Return_False()
        {
            // Arrange
            string badID = null;

            // Act
            var result = TestHelper.ProductService.AddComment(badID, "hey jude");

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for addComment with product that does not exist
        /// </summary>
        [Test]
        public void AddComment_Invalid_Null_Product_Should_Return_False()
        {
            // Arrange
            string badID = "bogus";

            // Act
            var result = TestHelper.ProductService.AddComment(badID, "hey jude");

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for addComment with null comment
        /// </summary>
        [Test]
        public void AddComment_Invalid_Null_Comment_Should_Return_False()
        {
            // Arrange
            var data = new ProductModel();

            var newData = TestHelper.ProductService.CreateProduct(data);

            // Act
            var result = TestHelper.ProductService.AddComment(newData.Id, null);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for addComment with empty comment
        /// </summary>
        [Test]
        public void AddComment_Invalid_Empty_Comment_Should_Return_False()
        {
            // Arrange
            var data = new ProductModel();

            var newData = TestHelper.ProductService.CreateProduct(data);

            // Act
            var result = TestHelper.ProductService.AddComment(newData.Id, "");

            // Reset
            TestHelper.ProductService.DeleteData(newData.Id);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for addComment with valid comment
        /// </summary>
        [Test]
        public void AddComment_Valid_Comment_Should_Return_True()
        {
            // Arrange
            var data = new ProductModel();

            var newData = TestHelper.ProductService.CreateProduct(data);

            // Act
            var result = TestHelper.ProductService.AddComment(newData.Id, "hey jude, dont make it bad");

            var comment = TestHelper.ProductService.GetProduct(newData.Id).CommentList.Last().Comment;

            var commentLen = TestHelper.ProductService.GetProduct(newData.Id).CommentList.Count;

            // Reset
            TestHelper.ProductService.DeleteData(newData.Id);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, commentLen);
            Assert.AreEqual("hey jude, dont make it bad", comment);
        }

        #endregion AddComment

        #region DeleteComment

        /// <summary>
        /// Unit test for delete comment with null product ID
        /// </summary>
        [Test]
        public void DeleteComment_Invalid_Null_ProductID_Should_Return_False()
        {
            // Arrange
            string badID = null;

            string commentId = "sss";

            // Act
            var result = TestHelper.ProductService.DeleteComment(badID, commentId);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for delete comment with product that does not exist
        /// </summary>
        [Test]
        public void DeleteComment_Invalid_Null_Product_Should_Return_False()
        {
            // Arrange
            string badID = "bogus";

            string commentId = "sss";

            // Act
            var result = TestHelper.ProductService.DeleteComment(badID, commentId);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for delete comment with comment that does not exist in the restaurant
        /// </summary>
        [Test]
        public void DeleteComment_Invalid_Bogus_CommentId_Should_Return_False()
        {
            // Arrange
            var data = new ProductModel();

            TestHelper.ProductService.CreateProduct(data);

            var newProduct = TestHelper.ProductService.GetProduct(data.Id);

            TestHelper.ProductService.AddComment(newProduct.Id, "testing");

            // Act
            var result = TestHelper.ProductService.DeleteComment(newProduct.Id, "bogus");

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Unit test for delete comment with valid product id and valid comment id
        /// </summary>
        [Test]
        public void DeleteComment_Valid_Good_CommentId_Should_Return_True()
        {
            // Arrange
            var data = new ProductModel();

            TestHelper.ProductService.CreateProduct(data);

            var newProduct = TestHelper.ProductService.GetProduct(data.Id);

            TestHelper.ProductService.AddComment(newProduct.Id, "testing");
            TestHelper.ProductService.AddComment(newProduct.Id, "testing");

            newProduct = TestHelper.ProductService.GetProduct(newProduct.Id);

            var commentId = newProduct.CommentList.Last().Id;

            // Act
            var result = TestHelper.ProductService.DeleteComment(newProduct.Id, commentId);

            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion DeleteComment

        #region GetProductsByTime

        /// <summary>
        /// Tests GetProductsByTime with default time
        /// </summary>
        [Test]
        public void GetProductsByTime_Valid_Default_Return_All_Products()
        {
            // Arrange
            var data = TestHelper.ProductService.GetAllData();

            var expected = data.Count();

            // Act
            var products = TestHelper.ProductService.GetProductsByTime();

            var actual = products.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests GetProductsByTime exclude products with null hours
        /// </summary>
        [Test]
        public void GetProductsByTime_Valid_With_Null_Hours_Products_Should_Return_List_Of_Products_Without_Null_Hours_Products()
        {
            // Arrange
            var data = TestHelper.ProductService.CreateProduct(new ProductModel()
            {
                Hours = null,
            });

            var id = data.Id;

            // Act
            var products = TestHelper.ProductService.GetProductsByTime(1);

            var actual = products.FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Tests GetProductsByTime exclude products current day of the week that have null hours
        /// </summary>
        [Test]
        public void GetProductsByTime_Valid_With_Day_Null_Hour_Products_Should_Return_List_Of_Products_Without_Day_Null_Hours_Products()
        {
            // Arrange
            var product = new ProductModel();

            product.Hours = new List<int[]>() { null, null, null, null, null, null, null };

            var data = TestHelper.ProductService.CreateProduct(product);

            var id = data.Id;

            // Act
            var products = TestHelper.ProductService.GetProductsByTime(1);

            var actual = products.FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Tests GetProductsByTime include products open after midnight with time within business hours
        /// </summary>
        [Test]
        public void GetProductsByTime_Valid_With_Open_After_Midnight_Should_Return_List_Of_Products_Open()
        {
            // Arrange
            var product = new ProductModel();

            product.Hours = new List<int[]>() {
                new int[] { 21, 2 }, new int[] { 21, 2 }, new int[] { 21, 2 },
                new int[] { 21, 2 }, new int[] { 21, 2 }, new int[] { 21, 2 },
                new int[] { 21, 2 } };

            var data = TestHelper.ProductService.CreateProduct(product);

            var id = data.Id;

            // Act
            var products = TestHelper.ProductService.GetProductsByTime(22);

            var actual = products.FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(product.Id, actual.Id);
        }

        /// <summary>
        /// Tests GetProductsByTime exclude products open after midnight with time after close
        [Test]
        public void GetProductsByTime_Valid_With_Open_After_Midnight_And_Time_After_Midnight_After_Close_Should_Return_List_Of_Products_Open()
        {
            // Arrange
            var product = new ProductModel();

            var product2 = new ProductModel();
            // Create a product that close at 2am
            product.Hours = new List<int[]>() {
                new int[] { 21, 2 }, new int[] { 21, 2 }, new int[] { 21, 2 },
                new int[] { 21, 2 }, new int[] { 21, 2 }, new int[] { 21, 2 },
                new int[] { 21, 2 } 
            };
            // Create a product that still open at 2am
            product2.Hours = new List<int[]>()
            {
                new int[] { 21, 3 }, new int[] { 21, 3 }, new int[] { 21 , 3 },
                new int[] { 21, 3 }, new int[] { 21, 3 }, new int[] { 21 , 3 },
                new int[] { 21, 3 }
            };

            var data = TestHelper.ProductService.CreateProduct(product);

            TestHelper.ProductService.CreateProduct(product2);

            var id = data.Id;

            // Act
            var products = TestHelper.ProductService.GetProductsByTime(2);
            // Only product that sill open at 2 am will be returned
            // so the product created that closed at 1 will not be in the list
            var actual = products.FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(null, actual);
        }

        #endregion GetProductsByTime

        #region Search

        /// <summary>
        /// Tests Search with search term string
        /// </summary>
        [Test]
        public void Search_Valid_Return_List_Of_Products()
        {
            // Arrange
            var title = "test-product-name";

            var title2 = "";

            var data = TestHelper.ProductService.CreateProduct(
                new ProductModel()
                {
                    Title = title,
                }
            );

            // Act
            var products = TestHelper.ProductService.Search(title);

            var products2 = TestHelper.ProductService.Search(title2);

            var actual = products.FirstOrDefault(p => p.Title == data.Title);

            var actual2 = products2.FirstOrDefault(p => p.Title == data.Title);

            // Assert
            Assert.AreEqual(data.Id, actual.Id);
            Assert.AreEqual(data.Id, actual2.Id);
        }

        #endregion Search
    }
}