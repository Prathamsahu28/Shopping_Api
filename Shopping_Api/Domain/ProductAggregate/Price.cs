using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace Shopping_Api.Domain.ProductAggregate
{
    public class Price : ValueObject
    {
        public int Value { get; }


        private Price(int value)

        {
            Value = value;
        }

        public static Result<Price> Create(int value)
        {

            if (value < 1)
                return Result.Failure<Price>("Quantity Is Not Negative Or Zero");

            return Result.Success(new Price(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(Price quantity)
        {
            return quantity.Value;
        }
        public static explicit operator Price(int Price)
        {
            return Create(Price).Value;
        }
    }
}
