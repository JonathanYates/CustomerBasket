namespace CustomerBasket.Models
{
    public interface IBasket
    {
        IList<BasketItem> Items { get; }
        void AddItem(Product product, int quantity);
        decimal CalculateTotal();
    }

}
