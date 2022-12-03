using Zero.SeedWorks;

namespace Zero.AspNetCoreServiceProjectExample.Domain.OrderAggregate
{
    public class Item : Entity, IAggregateRoot
    {
        public long ItemId { get; private set; }

        public string ItemName { get; private set; }

        public int Quantity { get; private set; }

        public int Price { get; private set; }




        public Item( string itemName , int quantity, int price)
        {

            
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
                
        }
    }
}
