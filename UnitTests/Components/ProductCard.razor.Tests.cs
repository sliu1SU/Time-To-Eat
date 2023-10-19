using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Bunit;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit test for ProductCard razor component
    /// </summary>
    public class ProductCardTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Initialize unit test 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region ProductCard 

        /// <summary>
        /// Unit test on ProductCard with default argument
        /// </summary>
        [Test]
        public void ProductCard_Valid_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var title = "This is a test title";

            var description = "This is a test description";

            // Create a test product with preset title
            var product = TestHelper.ProductService.CreateProduct(
                new ProductModel() 
                { Title = title, Description = description });
            
            // Act

            // pre-render the component
            var page = RenderComponent<ProductCard>(parameters => parameters.Add(p => p.Product, product));

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains(title));
        }

        /// <summary>
        /// Unit test on ProductList with product that has existing 1 rating
        /// </summary>
        [Test]
        public void ProductCard_Valid_ID_With_Ratings_One_Vote_Count_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var title = "This is a test title";

            // Create a test product with preset title and one rating
            var product = TestHelper.ProductService.CreateProduct(
                new ProductModel()
                {
                    Title = title,
                    Ratings = new int[] { 1 }
                });

            // get the product id
            var id = product.Id;

            //Act
            
            // pre-render the component
            var page = RenderComponent<ProductCard>(parameter => parameter.Add(p => p.Product, product));

            // Get the Cards returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains(title));
        }

        /// <summary>
        /// This test tests that the SubmitRating will change the vote as well as the Star checked
        /// Because the star check is a calculation of the ratins, using a record that has no stars and checking one makes clear what was changed
        /// 
        /// The test needs to open the page
        /// Then open the popup on the card
        /// Then record the state of the count and star check status
        /// Then check a star
        /// Then check again the state of the count and star check status
        /// </summary>
        [Test]
        public void ProductCard_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // pre click title
            var preTitle = "Be the first to vote!";

            // post click title
            var postTitle = "1 Vote";

            // create a product with no ratings
            var product = TestHelper.ProductService.CreateProduct(
                new ProductModel()
            );

            // get the product id
            var id = product.Id;

            // pre-render the component
            var page = RenderComponent<ProductCard>(parameter => parameter.Add(p => p.Product,product));

            // Find all the stars
            var staredButtons = page.FindAll("span");

            // Get the pre vote count
            var preVoteCountSpan = staredButtons[0];

            // Get the vote count, the list should have 7 elements, element 2 is the string for the count
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the first star item from the list, it should not be checked
            var staredButton = staredButtons.First(b => !string.IsNullOrEmpty(b.ClassName) && b.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = staredButton.OuterHtml;

            // Act
            staredButton.Click();

            // Find all the stars
            staredButtons = page.FindAll("span");

            // Get the post vote count
            var postVoteCountSpan = staredButtons[0];

            // Get the vote count, the list should have 7 elements, element 2 is the string for the count
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the last stared item from the list
            staredButton = staredButtons.First(b => !string.IsNullOrEmpty(b.ClassName) && b.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = staredButton.OuterHtml;

            // Assert
            Assert.AreEqual(true, preVoteCountString.Contains(preTitle));
            Assert.AreEqual(true, postVoteCountString.Contains(postTitle));
            Assert.AreEqual(false, postVoteCountString.Equals(preVoteCountString));
        }

        /// <summary>
        /// This test tests that the SubmitRating will change the vote as well as the Star checked
        /// Because the star check is a calculation of the ratigns, using a record that has existing stars and checking one makes clear what was changed
        /// 
        /// The test needs to open the page
        /// Then open the popup on the card
        /// Then record the state of the count and star check status
        /// Then check a star
        /// Then check again the state of the count and star check status
        /// </summary>
        [Test]
        public void ProductCard_Valid_ID_Click_Existing_stared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // pre click title
            var preTitle = "1 Vote";

            // post click title
            var postTitle = "2 Votes";

            // Create a product with 1 rating
            var product = TestHelper.ProductService.CreateProduct(
                new ProductModel() { Ratings = new int[] { 1 } }
            );

            // get the product id
            var id = product.Id;

            // pre-render the component
            var page = RenderComponent<ProductCard>(parameter => parameter.Add(p => p.Product, product));

            // Find all the stars
            var staredButtons = page.FindAll("span");

            // Get the pre vote count
            var preVoteCountSpan = staredButtons[0];

            // Get the vote count, the list should have 7 elements, element 2 is the string for the count
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the first star item from the list, it should not be checked
            var staredButton = staredButtons.First(b => !string.IsNullOrEmpty(b.ClassName) && b.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = staredButton.OuterHtml;

            // Act
            staredButton.Click();

            // Find all the stars
            staredButtons = page.FindAll("span");

            // Get the post vote count
            var postVoteCountSpan = staredButtons[0];

            // Get the vote count, the list should have 7 elements, element 2 is the string for the count
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the last stared item from the list
            staredButton = staredButtons.First(b => !string.IsNullOrEmpty(b.ClassName) && b.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = staredButton.OuterHtml;

            // Assert
            Assert.AreEqual(true, preVoteCountString.Contains(preTitle));
            Assert.AreEqual(true, postVoteCountString.Contains(postTitle));
            Assert.AreEqual(false, postVoteCountString.Equals(preVoteCountString));
        }

        #endregion ProductCard
    }
}