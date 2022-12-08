using Zero.SeedWorks;
namespace Shopping_Api.Domain.ShoppingCartAggregate
{
    public class ShoppingCart : Entity ,IAggregateRoot
    {

        public int CartId { get; private set; }
        public int CustomerId { get; private set; }

        public int ItemId { get; private set; }

        public string ItemName { get; private set; }    
        public int Quantity { get; private set; }

        public int Price { get; private set; }



        public ShoppingCart ( int customerId  , int itemId, string itemName , int quantity ,int price)
        {
            CustomerId = customerId;    
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;    
            Price = price;

        }
         

    }
}
