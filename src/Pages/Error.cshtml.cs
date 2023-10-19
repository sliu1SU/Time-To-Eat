using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Error page model
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // Request Id
        public string RequestId { get; set; }

        /// <summary>
        /// Check if request id is null or empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Loggerfor ErrorModel
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Default contructor for ErrorModel
        /// </summary>
        /// <param name="logger">logger dependecy for ErrorModel</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// OnGet method for ErrorModel
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}