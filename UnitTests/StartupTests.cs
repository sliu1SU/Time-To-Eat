using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;

namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// Unit test for Startup
    /// </summary>
    public class StartupTests
    {
        #region TestSetup

        /// <summary>
        /// Initialize the test for Startup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Configure Startup
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }

        #endregion TestSetup

        #region ConfigureServices

        /// <summary>
        /// Test Startup configure services
        /// WebHost should not be null
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion ConfigureServices

        #region Configure

        /// <summary>
        /// Test Startup configure
        /// /// WebHost should not be null
        /// </summary>
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion Configure
    }
}