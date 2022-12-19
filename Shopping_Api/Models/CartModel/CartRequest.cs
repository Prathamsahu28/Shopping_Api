namespace Shopping_Api.Models
{
    public class CartRequest
    {
        public int CustomerId { get; set; }


        public int ProductId { get; set; }
        

        public int Quantity { get; set; }
    }
}
