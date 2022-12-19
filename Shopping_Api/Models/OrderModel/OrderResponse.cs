using Shopping_Api.Domain.OrderAggregate;

namespace Shopping_Api.Models.OrderModel
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public DateTime OrderDateTime { get; set; }

        public int CustomerId { get;  set; }
        public int TotalNumberOfItems { get; set; }
        public int TotalPrice { get; set; } 

        //public List<OrderItem> OrderProducts { get; set; }
        //public string CustomerName { get; set; }

       // public string ProductName { get; set; }
        //public int Quantity { get; set; }
        //public int Price { get; set; }


        //public int  TotalAmount { get; set; }




    }
}
