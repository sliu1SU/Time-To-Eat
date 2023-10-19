using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// JsonFileProductService class
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Initiating the webhost environment for the application
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Call the IWebHostEnvironment object
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Get the file path and filename of product data for loading
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// GetAllData function to get all product from the database
        /// </summary>
        /// <returns>IEnumerable<ProductModel></returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Specific method name that call GetAllData method
        /// </summary>
        /// <returns>List of all Products in file data storage</returns>
        public IEnumerable<ProductModel> GetProducts()
        {
            return GetAllData();
        }

        /// <summary>
        /// Insert a new product into the data storage
        /// </summary>
        public ProductModel CreateProduct(ProductModel product)
        {
            product.Id = System.Guid.NewGuid().ToString();

            var products = GetAllData();
            products = products.Append(product);

            SaveData(products);

            return product;
        }

        /// <summary>
        /// Searches for a product 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> Search(string searchTerm)
        {
            var products = GetAllData();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return products;
            }
            return products.Where(p => p.Title != null && p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieve a single product in the data storage
        /// </summary>
        /// <param name="id">The id of the product to retrive</param>
        /// <returns>The product with the given id or null if not found</returns>
        public ProductModel GetProduct(string id)
        {
            return GetAllData().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// Add Rating
        /// 
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));

            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Add comment to a restaurant
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userComment"></param>
        /// <returns></returns>
        public bool AddComment(string productId, string userComment)
        {
            // If the ProductID is invalid, return false
            if (string.IsNullOrEmpty(productId)) 
            { 
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));

            // If the product does not exist in the database, return false
            if (data == null)
            {
                return false;
            }

            if (userComment == null)
            {
                return false;
            }

            if (userComment == "")
            {
                return false;
            }

            // Add the Comment to the Array
            var commentObject = new CommentModel();
            commentObject.Comment = userComment;

            var comments = data.CommentList;
            comments.Add(commentObject);

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Delete a comment from a restaurant
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="toBeDelete"></param>
        /// <returns>bool</returns>
        public bool DeleteComment(string productId, string commentId)
        {
            // If the ProductID is invalid, return false
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));

            // If the product does not exist in the database, return false
            if (data == null)
            {
                return false;
            }

            // If commentList does not contain commentId, return false
            var commentArr = data.CommentList.ToArray();

            for (int i = 0; i < commentArr.Length; i++)
            {
                if (commentId == commentArr[i].Id)
                {
                    var toBeDelete = commentArr[i];

                    data.CommentList.Remove(toBeDelete);

                    break;
                }

                if (i == commentArr.Length - 1)
                {
                    return false;
                }
            }

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();

            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));

            if (productData == null)
            {
                return null;
            }

            // Update the data to the new passed in values
            productData.Title = data.Title;
            productData.Description = data.Description.Trim();
            productData.Url = data.Url;
            productData.Image = data.Image;
            productData.Quantity = data.Quantity;
            productData.Price = data.Price;
            productData.CommentList = data.CommentList;

            SaveData(products);

            return productData;
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        private void SaveData(IEnumerable<ProductModel> products)
        {
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns>ProductModel</returns>
        public ProductModel CreateData()
        {
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
            };

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns>ProductModel</returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();

            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            SaveData(newDataSet);

            return data;
        }

        /// <summary>
        /// Filter all restaurant that have the given time within business hours
        /// </summary>
        /// <param name="time">The time to filter on</param>
        /// <returns>List of restaurants that are open at the given time</returns>
        public IEnumerable<ProductModel> GetProductsByTime(int time=0)
        {
            var products = GetAllData();

            if (time == 0)
            {
                return products;
            }

            DateTime thisDay = DateTime.Today;

            var dayOfWeek = thisDay.DayOfWeek.ToString();

            var dayIndexes = new Dictionary<string, int>
            {
                {"Monday", 0},
                {"Tuesday", 1},
                {"Wednesday", 2},
                {"Thursday", 3},
                {"Friday", 4},
                {"Saturday", 5},
                {"Sunday", 6}
            };

            var dayIndex = dayIndexes[dayOfWeek];

            var result = new List<ProductModel>();

            foreach (var product in products)
            {
                if (product.Hours == null) {
                    continue;
                }

                var hours = product.Hours[dayIndex];

                if (hours == null)
                {
                    continue;
                }

                var open = hours[0];

                var close = hours[1];

                if (open > close)
                {
                    close += 24;
                    if (time < open)
                    {
                        time += 24;
                    }
                }
                
                if (time < open)
                {
                    continue;
                }

                if (time >= close)
                {
                    continue;
                }
               
                result.Add(product);
                
            }
            return result;
        }        
    }
}