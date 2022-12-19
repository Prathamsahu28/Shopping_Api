using Shopping_Api.Domain.ShoppingCartAggregate;
using System.Linq.Expressions;
using Zero.SeedWorks;

namespace Shopping_Api.Specifications
{
    public class CartByCustomerIdSpecification : BaseSpecification<Cart>
    {
        public CartByCustomerIdSpecification(int id ) : base(x => x.CustomerId == id  )
        {
        }
    }
}
