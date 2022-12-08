using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace Zero.Shopping_Api.Domain.ItemAggregate
{
    public class Item : Entity, IAggregateRoot
    {
        public int ItemId { get; private set; }

        public string ItemName { get; private set; }

        public int Quantity { get; private set; }

        public int Price { get; private set; }




        public Item( string itemName , int quantity, int price)
        {

            
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
                
        }

        public Result Update(int quantity)
        {
            

            Quantity = quantity;

           

            return Result.Success();
        }
    }
}
