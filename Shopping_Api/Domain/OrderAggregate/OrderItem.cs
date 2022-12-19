using Shopping_Api.Domain.ProductAggregate;
using Zero.SeedWorks;

namespace Shopping_Api.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {

        //public int ProductId { get; set; }

        // 

        //public Quantity Quantity { get; set; }



        //public int Price { get; set; }


        //public string ProductName { get; private set; }
        public int OrderItemId { get; private set; }

        public string ProductName { get; private set; }
        public int ProductId { get; private set; }

        public Quantity Quantity { get; private set; }

        public int Price { get; private set; }

       

        public OrderItem(int productId,string productName , Quantity quantity ,int price ) 
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}