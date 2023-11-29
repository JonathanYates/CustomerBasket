namespace CustomerBasket.Models.Discount;

public class PriceDiscount(DiscountTrigger discountTrigger, DiscountTarget discountTarget) : IDiscount
{
    public decimal ApplyDiscount(IBasket basket)
    {
        var triggerProducts = basket.Items.Where(item => item.Product.Name == discountTrigger.ProductName).ToArray();

        var targetProducts = basket.Items
            .Where(item => item.Product.Name == discountTarget.ProductName)
            .ToArray();

        if (triggerProducts.Length == 0 || targetProducts.Length == 0)
        {
            return 0;
        }

        var triggerProductCount = triggerProducts.Sum(item => item.Quantity);
        var itemsToDiscount = triggerProductCount / discountTrigger.Quantity;
        var targetProductCount = targetProducts.Sum(item => item.Quantity);

        if (itemsToDiscount > 0 && targetProductCount >= itemsToDiscount)
        {
            var targetPrice = targetProducts.First().Product.Price;
            var discount = targetPrice * discountTarget.Discount * itemsToDiscount;
            return discount;
        }

        return 0;
    }
}