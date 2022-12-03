using Zero.SharedKernel.Types.Result;

namespace Zero.AspNetCoreServiceProjectExample.Errors
{
    public class customerError : Error
    {
        public customerError(string message) : base(message)
        {

        }

    }
}
