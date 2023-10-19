using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Restaurants
{
    /// <summary>
    /// The Detail User page model for the restaurants.
    /// </summary>
    public class DetailUserModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // The data to show
        public ProductModel Product;

        // Definition for SearchTerm 
        [BindProperty(SupportsGet = true)]
        public string Comment { get; set; } = "";

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="productService"></param>
        public DetailUserModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            // Add comment if user comment is not empty
            if (Comment != "")
            {
                ProductService.AddComment(id, Comment);
                return RedirectToPage("/Restaurants/DetailUser", new { id = id });
            }

            Product = ProductService.GetProduct(id);
            return Page();
        }
    }
}