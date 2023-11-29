namespace CustomerBasket.Models.Discount;

public class FreeProductDiscount(DiscountTrigger discountTrigger) : IDiscount
{
    public decimal ApplyDiscount(IBasket basket)
    {
        var triggerProducts = basket.Items
            .Where(item => item.Product.Name == discountTrigger.ProductName)
            .ToArray();

        if (triggerProducts.Length == 0)
        {
            return 0;
        }

        var triggerProductCount = triggerProducts.Sum(item => item.Quantity);

        var itemsToDiscount = triggerProductCount / discountTrigger.Quantity;

        if (itemsToDiscount > 0)
        {
            var targetPrice = triggerProducts.First().Product.Price;
            var discount = itemsToDiscount * targetPrice;
            return discount;
        }

        return 0;
    }
}