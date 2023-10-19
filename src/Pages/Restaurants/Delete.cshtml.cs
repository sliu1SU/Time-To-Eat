using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Pages.Restaurants
{
    /// <summary>
    /// The delete model page for the a restaurant.
    /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // Bind the data for the view
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Default Construtor for DeleteModel with middleware injected.
        /// </summary>
        /// <param name="productService">Product Service dependency injection</param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        
        /// <summary>
        /// REST Get request to show to-be-deleted product info
        /// </summary>
        public void OnGet(string id)
        {
            // Get the product from the data storage
            Product = ProductService.GetProduct(id);
        }

        /// <summary>
        /// REST Post request to delete existing product
        /// </summary>
        /// <returns>Redirect to Index Page after deletion</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.DeleteData(Product.Id);

            // Redirect user to the deleted restaurant detail page
            return RedirectToPage("./Index");
        }
    }
}