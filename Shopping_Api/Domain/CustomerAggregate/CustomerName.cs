using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace Zero.AspNetCoreServiceProjectExample.Domain
{
    public class CustomerName : ValueObject
    {
        public string Value { get; }
        public static readonly char[] _notAllowedCharacters = new char[] { '$', '^', '`', '<', '>', '+', '/', '=', '~' };

        private CustomerName(string value)
        {
            Value = value;
        }
        public static Result<CustomerName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<CustomerName>("Name Can't Be Blank.");
            if (value.Length > 100)
                return Result.Failure<CustomerName>("Name Is Too Long.");
            if (value.IndexOfAny(_notAllowedCharacters) != -1)
                return Result.Failure<CustomerName>("Some special characters are not allowed in the name.");

            return Result.Success(new CustomerName(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator string(CustomerName name)
        {
            return name.Value;
        }
        public static explicit operator CustomerName(string name)
        {
            return Create(name).Value;
        }

    }
}
