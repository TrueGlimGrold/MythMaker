namespace MythMaker.Models
{
    public class ViewModel
    {
        public Category CurrentCategory { get; set; }
        public List<Category> Categories { get; set; }

        public Product CurrentProduct { get; set; }
        public List<Product> Products { get; set; }

        public List<ShoppingCart> CartItems { get; set; }
    }
}
