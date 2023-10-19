using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Restaurants
{
    /// <summary>
    /// The detail page model for the restaurants.
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        // The data to show
        public ProductModel Product;

        // Definition for SearchTerm 
        [BindProperty(SupportsGet = true)]
        public string Comment { get; set; } = "";

        // The business hours to be displayed
        public List<string> Hours { get; set; }

        // The days of the week
        public List<string> Days { get; set; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="productService"></param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
            Days = new List<string> { 
                "Monday", "Tuesday", "Wednesday", 
                "Thursday", "Friday", "Saturday", "Sunday" };
            Hours = new List<string>();
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
                return RedirectToPage("/Restaurants/Detail", new { id = id });
            }

            Product = ProductService.GetProduct(id);

            if (Product == null)
            {
                return RedirectToPage("/Restaurants/Index");
            }

            if (Product.Hours == null)
            {
                Hours = new List<string>() { "NA", "NA", "NA", "NA", "NA", "NA", "NA" };
                return Page();
            }

            foreach (var hour in Product.Hours) {
                if (hour == null) 
                {
                    Hours.Add("Closed");
                    continue;
                }

                if (hour != null)
                {
                    int idx = 0;

                    string openHours = "";

                    foreach (var time in hour)
                    {
                        if (time > 12)
                        {
                            openHours += (time - 12).ToString() + ":00 PM";
                        }

                        if (time <= 12)
                        {
                            openHours += time.ToString() + ":00 AM";
                        }

                        idx++;

                        if (idx < hour.Length)
                        {
                            openHours += " - ";
                        }
                    }

                    Hours.Add(openHours);
                }
            }

            return Page();
        }
    }
}