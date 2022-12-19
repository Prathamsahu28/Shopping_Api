using Zero.SeedWorks;
namespace Shopping_Api.Domain.ShoppingCartAggregate
{
    public class Cart : Entity ,IAggregateRoot
    {

       //public int CartId { get; private set; }
        public int CustomerId { get; private set; }

        public int ProductId { get; private set; }

        public string ProductName { get; private set; } 

     
        public Quantity Quantity { get; private set; }

        public int Price { get; private set; }  

        private Cart()
        {
        }



        public Cart ( int customerId  , int itemId, string productName,   Quantity quantity ,int price )
        {
            CustomerId = customerId;
            ProductId = itemId;
            ProductName = productName;
            
            Quantity = quantity; 
            Price = price;
          

        }
         

    }
}
