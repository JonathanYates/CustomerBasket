namespace CustomerBasket.Models
{
    public class BasketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public BasketItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
