namespace LanguageFeatures.Extensions
{
    public static class ShoppingCartExtension
    {
        //public static decimal TotalPrices(this ShoppingCart cart)
        //{
        //    decimal total = 0;
        //    if (cart?.Products != null)
        //        foreach (Product? product in cart.Products)
        //            total += product.Price ?? 0;

        //    return total;
        //}

        public static decimal TotalPrices(this IEnumerable<Product?> products)
        {
            decimal total = 0;
            foreach (Product? product in products)
                total += product?.Price ?? 0;

            return total;
        }

        public static IEnumerable<Product?> FilterByPrice(this IEnumerable<Product?> products, decimal minimumPrice)
        {
            foreach (Product? product in products)
            {
                if ((product?.Price ?? 0) >= minimumPrice)
                    yield return product;
            }
        }

        public static IEnumerable<Product?> FilterByName(this IEnumerable<Product?> products, char firstLetter)
        {
            foreach (Product? product in products)
            {
                if (product?.Name[0] == firstLetter)
                    yield return product;
            }
        }

        public static IEnumerable<Product?> Filter(this IEnumerable<Product?> productEnum, Func<Product?, bool> selector)
        {
            foreach (var product in productEnum)
                if (selector(product))
                    yield return product;
        }
    }
}
