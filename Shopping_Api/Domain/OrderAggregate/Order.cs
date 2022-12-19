using Zero.SeedWorks;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Domain.OrderAggregate
{
    public class Order :Entity ,IAggregateRoot
    {
       

        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; }

        public int CustomerId { get; private set; }

        public int NumberOfItems { get; private set; }

        public int TotalPrice { get; private set; }

        private List<OrderItem> _items = new();
        public IReadOnlyList<OrderItem> OrderProducts => _items.AsReadOnly();

       // public List<OrderItem> OrderProducts => _items;



        private Order()
        {
        }

       public Order(int customerId ,int numberOfOrders, int totalPrice, DateTime orderDate, List<(int productId, string productName, Quantity quantity, int totalPrice)> orderProducts)
       {
          
         CustomerId = customerId;
         NumberOfItems = numberOfOrders;
           TotalPrice= totalPrice;
         OrderDate = orderDate; 
            foreach(var item in orderProducts)
            {
                _items.Add(new OrderItem(item.productId ,item.productName, item.quantity , item.totalPrice));
            }


       }

     
    }
}
