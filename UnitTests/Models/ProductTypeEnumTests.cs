using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

namespace UnitTests.Models
{
    /// <summary>
    /// Unit test for product type enum 
    /// </summary>
    internal class ProductTypeEnumTests
    {
        #region TestSetup

        /// <summary>
        /// Setup the test environment for the TestModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region DisplayName

        /// <summary>
        /// Test DisplayName function with enum type undefined
        /// </summary>
        [Test]
        public void DisplayName_Valid_ProductType_Undefined_Should_Return_EmptyString()
        {
            // Arrange
            var productType = ProductTypeEnum.Undefined;

            var expected = "";

            // Act
            var actual = ProductTypeEnumExtensions.DisplayName(productType);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Test DisplayName function with enum type Fastfood
        /// </summary>
        [Test]
        public void DisplayName_Valid_ProductType_Fastfood_Should_Return_Fastfood()
        {
            // Arrange
            var productType = ProductTypeEnum.Fastfood;

            var expected = "Fastfood";

            // Act
            var actual = ProductTypeEnumExtensions.DisplayName(productType);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Test DisplayName function with enum type Cafe
        /// </summary>
        [Test]
        public void DisplayName_Valid_ProductType_Cafe_Should_Return_Cafe()
        {
            // Arrange
            var productType = ProductTypeEnum.Cafe;

            var expected = "Cafe";

            // Act
            var actual = ProductTypeEnumExtensions.DisplayName(productType);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Test DisplayName function with enum type BBQ
        /// </summary>
        [Test]
        public void DisplayName_Valid_ProductType_BBQ_Should_Return_BBQ()
        {
            // Arrange
            var productType = ProductTypeEnum.BBQ;

            var expected = "BBQ";

            // Act
            var actual = ProductTypeEnumExtensions.DisplayName(productType);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Test DisplayName function with enum type FineDining
        /// </summary>
        [Test]
        public void DisplayName_Valid_ProductType_FineDining_Should_Return_FineDining()
        {
            // Arrange
            var productType = ProductTypeEnum.FineDining;

            var expected = "FineDining";

            // Act
            var actual = ProductTypeEnumExtensions.DisplayName(productType);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        #endregion DisplayName
    }
}