using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Restaurants
{
    /// <summary>
    /// The index model page to display all restauraunts in the database
    /// </summary>
    public class IndexModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // The data to show on Restaurants Index Page
        public IEnumerable<ProductModel> Products;

        // Definition for SearchTerm 
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="productService"></param>
        public IndexModel(JsonFileProductService productService)
        {
            // Product services from JsonFileProductService 
            ProductService = productService;
        }

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public void OnGet()
        {
            // Products returned from performing search operation 
            Products = ProductService.Search(SearchTerm);
        }
    }
}