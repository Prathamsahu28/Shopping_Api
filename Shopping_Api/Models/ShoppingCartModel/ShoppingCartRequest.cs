namespace Shopping_Api.Models
{
    public class ShoppingCartRequest
    {
        public int CustomerId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
