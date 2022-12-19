using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace Shopping_Api.Domain
{
    public class Quantity : ValueObject
    {
        

        public int Value { get; }

        
        private Quantity( int value)

        {
            Value = value;
        }

        public static Result<Quantity> Create(int value)
        {
           
            if (value < 1)
                return Result.Failure<Quantity>("Quantity Is Not Negative");
          
            return Result.Success(new Quantity(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(Quantity quantity)
        {
            return quantity.Value;
        }
        public static explicit operator Quantity(int quantity)
        {
            return Create(quantity).Value;
        }
    }
}
