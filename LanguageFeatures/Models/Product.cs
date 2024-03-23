namespace LanguageFeatures.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;

        public decimal? Price { get; set; }

        //public static Product?[]? GetProducts() -> null values and null array are OK
        public static Product?[]? GetProducts()
        {
            Product kayak = new()
            {
                Name = "Kayak",
                Price = 275M
            };

            var lifeJacket = new Product { Name = "LifeJAcket", Price = 48.95M };

            return new Product?[] { kayak, lifeJacket, null };
        }
    }
}
