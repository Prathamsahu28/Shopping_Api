namespace Zero.AspNetCoreServiceProjectExample.Errors
{
    public class DeletedPersonError : customerError
    {
        public DeletedPersonError() : base("Person Is Deleted. ")
        {

        }
        public DeletedPersonError(string message) : base(message)
        {

        }
    }
}
