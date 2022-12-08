using Zero.Shopping_Api.Domain;
using Zero.SeedWorks;

namespace Zero.Shopping_Api.Specifications

{
    public class ActivePersonSpecification : BaseSpecification<Customer>
    {
        public ActivePersonSpecification() : base(m => !m.IsDeleted)
        {
        }
    }
}
