namespace CustomerBasket.Models.Discount
{
    public interface IDiscount
    {
        decimal ApplyDiscount(IBasket basket);
    }
}
