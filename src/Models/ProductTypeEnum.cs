namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Product Types Enumeration
    /// </summary>
    public enum ProductTypeEnum
    {
        Undefined = 0,  // Default value
        Fastfood = 1,  // Fastfood Restaurant
        Cafe = 5,  // Cafe Restaurant
        BBQ = 130,  // BBQ Restaurant
        FineDining = 55,  // Fine Dining Restaurant
    }

    /// <summary>
    /// Product Type Functionalities 
    /// </summary>
    public static class ProductTypeEnumExtensions
    {   
        /// <summary>
        /// Get Product Type Name from Product Type enum
        /// </summary>
        public static string DisplayName(this ProductTypeEnum data)
        {
            return data switch
            {
                ProductTypeEnum.Fastfood => "Fastfood",
                ProductTypeEnum.Cafe => "Cafe",
                ProductTypeEnum.BBQ => "BBQ",
                ProductTypeEnum.FineDining => "FineDining",
 
                // Default, Unknown
                _ => "",
            };
        }
    }
}