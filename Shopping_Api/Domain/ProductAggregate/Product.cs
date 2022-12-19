using Shopping_Api.Domain;
using Shopping_Api.Domain.ProductAggregate;
using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace Zero.Shopping_Api.Domain.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        public int ProductId { get; private set; }

        public ProductName ProductName { get; private set; }

        public Quantity Quantity { get; private set; }

        public Price Price { get; private set; }





        public Product(ProductName itemName , Quantity quantity, Price price)
        {


            ProductName = itemName;
            Quantity = quantity;
            Price = price;
                
        }

        private Product()
        {
        }

        public Result Update(Quantity quantity)
        {
            

            Quantity = quantity;

           

            return Result.Success();
        }
    }
}
