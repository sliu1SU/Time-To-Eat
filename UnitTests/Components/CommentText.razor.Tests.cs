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
    /// Unit test for CommentText razor component
    /// </summary>
    public class CommentTextTests : BunitTestContext
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

        #region CommentText 

        /// <summary>
        /// Unit test on CommentText component with default argument
        /// </summary>
        [Test]
        public void CommentText_Valid_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var text = "This is a test comment";

            // Create a test Comment
            var comment = new CommentModel();
            comment.Comment = text;
            
            // pre-render the component
            var page = RenderComponent<CommentText>(
                parameters => parameters.Add(p => p.Comment, comment));

            // Act

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains(text));
        }

        /// <summary>
        /// Unit test on CommentText component to modify and save the comment
        /// </summary>
        [Test]
        public void CommentTest_Valid_ID_Click_Save_Should_Update_Current_Comment_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var text = "This is a test comment";

            // Create a test Comment
            var comment = new CommentModel();

            comment.Comment = text;

            var product = new ProductModel()
            {
                Title = "test-title-comment-test",
                Description = "test-description-comment-test"
            };
            product.CommentList.Add(comment);
            TestHelper.ProductService.CreateProduct(product);

            // pre-render the component
            var page = RenderComponent<CommentText>(
                parameters => parameters.Add(p => p.Comment, comment));

            var preUpdateText = page.Find("span").OuterHtml;

            // SAVE UNMODIFIED CONTENT

            // find all the buttons: Edit, Delete
            // get the last button (the one for the product just created)
            var buttons = page.FindAll("Button");

            var editButton = buttons.First();

            // Click Edit
            editButton.Click();

            // get Save Button
            buttons = page.FindAll("Button");

            var saveButton = buttons.First();

            // Click Save without modifying comment content
            // return to the main with Edit and Delete button
            saveButton.Click();

            // CANCEL

            // find all the buttons: Edit, Delete
            // get the last button (the one for the product just created)
            buttons = page.FindAll("Button");
            editButton = buttons.First();

            // Click Edit
            editButton.Click();

            // get cancelButton
            buttons = page.FindAll("Button");

            var cancelButton = buttons.Last();

            // Click Cancel
            cancelButton.Click();

            // TEST SAVE MODIFIED CONTENT

            // find all the buttons: Edit, Delete
            // get the last button (the one for the product just created)
            buttons = page.FindAll("Button");
            editButton = buttons.First();

            // Click Edit
            editButton.Click();

            // change comment text
            var modifiedText = text + "-modified";

            var commentInput = page.Find("input");

            commentInput.Change(modifiedText);

            // get saveButton
            buttons = page.FindAll("Button");
            saveButton = buttons.First();

            // Act

            // save edited comment and go back to main ui with Edit and Delete
            saveButton.Click();

            // get the post update html text
            var postUpdateText = page.Find("span").OuterHtml;

            // TEST DELETE
            // find all the buttons: Edit, Delete
            // get the last button (the one for the product just created)
            buttons = page.FindAll("Button");
            var deleteButton = buttons.Last();

            // Click Delete
            deleteButton.Click();
            buttons = page.FindAll("Button");
            var confirmButton = buttons.First();
            
            // click Confirm
            confirmButton.Click();

            // Assert
            Assert.AreEqual(false, postUpdateText.Equals(preUpdateText));
            Assert.AreEqual(true, postUpdateText.Contains(modifiedText));
        }

        #endregion CommentText
    }
}