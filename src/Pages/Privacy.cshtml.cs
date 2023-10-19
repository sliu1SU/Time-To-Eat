using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Allows attributes for razor page 
    /// </summary>
    public class PrivacyModel : PageModel
    {
        // The following methods are simply implementations of 
        // C#http calls to our logger, productservice so that we can display to page for viewing
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Creates a logger, ILogger<PrivacyModel>, 
        /// which uses a log category of the fully qualified name of the type PrivacyModel
        /// </summary>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// When a user makes a GET request to a page, we invoke this method
        /// </summary>
        public void OnGet()
        {
        }
    }
}