using CustomerBasket.Models;
using CustomerBasket.Models.Discount;

namespace CustomerBasketTests
{
    [TestClass]
    public class BasketTests
    {
        private readonly Product _bread = new Product("Bread", 1.00m);
        private readonly Product _butter = new Product("Butter", 0.80m);
        private readonly Product _milk = new Product("Milk", 1.15m);
        private Basket _basket;

        [TestInitialize]
        public void SetUp()
        {
            _basket = new Basket();

            _basket.AddDiscount(new PriceDiscount(
                new DiscountTrigger("Butter", 2),
                new DiscountTarget("Bread", 0.5m)));

            _basket.AddDiscount(new FreeProductDiscount(new DiscountTrigger("Milk", 4)));
        }

        [TestMethod]
        public void AddsProductsToBasketAndCalculatesTotal()
        {
            _basket.AddItem(_bread, 1);
            _basket.AddItem(_butter, 1);
            _basket.AddItem(_milk, 1);
            
            var total = _basket.CalculateTotal();

            Assert.AreEqual(2.95m, total);
        }

        [TestMethod]
        public void AddsBreadAndButterToBasketAndAppliesDiscount()
        {
            _basket.AddItem(_bread, 2);
            _basket.AddItem(_butter, 2);

            var total = _basket.CalculateTotal();

            Assert.AreEqual(3.10m, total);
        }

        [TestMethod] 
        public void AddsMilkToBasketAndAppliesDiscount()
        {
            _basket.AddItem(_milk, 4);

            var total = _basket.CalculateTotal();

            Assert.AreEqual(3.45m, total);
        }

        [TestMethod]
        public void AddsProductsToBasketAndAppliesDiscount()
        {
            /*
                2 butter @ 0.8 = £1.60
                1 bread @ £1 with discount of 0.5 = £0.50
                8 milk @ £1.15 with discount of 2 free milk cos we bought 2 * 4 = £6.90
                Total should be 4 + 2 + 8.05 = £14.05
             */

            _basket.AddItem(_butter, 2);
            _basket.AddItem(_bread, 1);
            _basket.AddItem(_milk, 8);

            var total = _basket.CalculateTotal();

            Assert.AreEqual(9.00m, total);
        }

        [TestMethod]
        public void AddsMultiDiscountedProductsToBasketAndAppliesDiscount()
        {
            /*
                5 butter @ £0.80 = £4
                3 bread @ £1 with discount of 2 * 0.5 = £2
                9 milk @ £1.15 with discount of 2 free milk cos we bought 2 * 4 = £8.05
                Total should be 4 + 2 + 8.05 = £14.05             
             */

            _basket.AddItem(_butter, 5);
            _basket.AddItem(_bread, 3);
            _basket.AddItem(_milk, 9);

            var total = _basket.CalculateTotal();

            Assert.AreEqual(14.05m, total);
        }
    }
}