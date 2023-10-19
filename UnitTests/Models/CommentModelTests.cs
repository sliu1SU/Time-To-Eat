using System;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

namespace UnitTests.Models
{   
    /// <summary>
    /// Unit test for Comment model Test
    /// </summary>
    internal class CommentModelTests
    {
        #region TestSetup

        /// <summary>
        /// Comment model instace to test 
        /// </summary>
        public static CommentModel TestModel;

        /// <summary>
        /// Set up the test environmenmt
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            TestModel = new CommentModel()
            {

            };
        }

        #endregion TestSetup

        #region TestID

        /// <summary>
        /// Unit test on Id set and get
        /// </summary>
        [Test]
        public void SetGetId_Valid_Should_Return_Id()
        {
            // Arrange
            var newId = Guid.NewGuid().ToString();

            // Act
            TestModel.Id = newId;

            // Assert
            Assert.AreEqual(newId, TestModel.Id);
        }

        #endregion TestID

        #region TestComment

        /// <summary>
        /// unit test on Comment set and get
        /// </summary>
        [Test]
        public void SetGetComment_Valid_Should_Return_Comment()
        {
            // Arrange
            var newComment = "test";

            // Act
            TestModel.Comment = newComment;

            // Assert
            Assert.AreEqual(newComment, TestModel.Comment);
        }

        #endregion TestComment
    }
}