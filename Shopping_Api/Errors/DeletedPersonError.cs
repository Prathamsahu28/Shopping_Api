namespace Zero.Shopping_Api.Errors
{
    public class DeletedPersonError : CustomerError
    {
        public DeletedPersonError() : base("Person Is Deleted. ")
        {

        }
        public DeletedPersonError(string message) : base(message)
        {

        }
    }
}
