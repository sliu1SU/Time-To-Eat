using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Net;

namespace ContosoCrafts.WebSite.Pages.Restaurants
{
    /// <summary>
    /// Create page
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // Bind the data for the form
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        
        /// <summary>
        /// REST Get request
        /// </summary>
        public void OnGet()
        {
            // Create a new empty product object (all fields are null)
            Product = new ProductModel();
        }

        /// <summary>
        /// Validate if input url is garbage
        /// </summary>
        /// <param name="url"></param>
        public bool IsGarbageUrl(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode != HttpStatusCode.OK;
            }
            catch
            {
                return true;
            }
        }
      
        /// <summary>
        /// REST Post request to create a new product
        /// </summary>
        /// <returns>Detail Page</returns>
        public IActionResult OnPost()
        {
             // Validate if image url is garbage
            if (IsGarbageUrl(Product.Image))
            {
                ModelState.AddModelError("Product.Image", "Image URL not valid.");
            }

            // Validate if website url is garbage
            if (IsGarbageUrl(Product.Url))
            {
                ModelState.AddModelError("Product.Url", "URL not an valid.");
            }
           
            
            // Proceed to create a new product if all the validation is passed
            if (ModelState.IsValid)
            {
                // Assign default value for product hours if not set
                Product.Hours = new List<int[]>();
                for (int i = 0; i < 7; i++)
                {
                    Product.Hours.Add(new int[2] {0, 24});
                }
                
                // Insert Product into database
                ProductModel product = ProductService.CreateProduct(Product);
                
                // Redirect user to the newly created restaurant detail page
                return RedirectToPage("./Detail", new {id = product.Id});
            }
            
            return Page();
        }
    }
}