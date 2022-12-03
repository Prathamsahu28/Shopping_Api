using Zero.AspNetCoreServiceProjectExample.Domain;
using Zero.SeedWorks;

namespace Zero.AspNetCoreServiceProjectExample.Specifications

{
    public class ActivePersonSpecification : BaseSpecification<Customer>
    {
        public ActivePersonSpecification() : base(m => !m.IsDeleted)
        {
        }
    }
}
