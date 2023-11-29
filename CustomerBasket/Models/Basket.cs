using CustomerBasket.Models.Discount;

namespace CustomerBasket.Models
{
    public class Basket : IBasket
    {
        public IList<BasketItem> Items { get; } = new List<BasketItem>();
        private readonly IList<IDiscount> _discounts = new List<IDiscount>();

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(basketItem => basketItem.Product.Name == product.Name);

            if (existingItem is not null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new BasketItem(product, quantity));
            }
        }

        public decimal CalculateTotal()
        {
            var basketTotal = Items.Sum(item => item.Quantity * item.Product.Price);
            var discounts = _discounts.Sum(discount => discount.ApplyDiscount(this));
            var discountedTotal = basketTotal - discounts;
            return discountedTotal;
        }

        public void AddDiscount(IDiscount discount)
        {
            _discounts.Add(discount);
        }
    }

}
