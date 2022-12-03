using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;
using Zero.AspNetCoreServiceProjectExample.Errors;

namespace Zero.AspNetCoreServiceProjectExample.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public long Id { get; private set; }
        public CustomerName Name { get; private set; }
      
        public MobileNumber? MobileNumber { get; private set; }
        public EmailAddress? EmailAddress { get; private set; }
        public bool IsDeleted { get; private set; }

        private Customer()
        {
        }

        /// <summary>
        /// Creates new person
        /// </summary>
        /// <param name="name">Name of the person</param>
        /// <param name="type">Type of the person</param>
        /// <param name="mobileNumber">Mobile number of the person</param>
        /// <param name="emailAddress">Email address of the person</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Customer(CustomerName name,  MobileNumber? mobileNumber, EmailAddress? emailAddress)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
          
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
            IsDeleted = false;
        }

        /// <summary>
        /// Updates person's details
        /// <para>
        /// Errors:
        /// <list type="bullet">
        /// <item><term><see cref="DeletedPersonError"/></term><description>If the person is deleted.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="name">Name of the person</param>
        /// <param name="type">Type of the person</param>
        /// <param name="mobileNumber">Mobile number of the person</param>
        /// <param name="emailAddress">Email address of the person</param>
        public Result Update(CustomerName name, MobileNumber? mobileNumber, EmailAddress? emailAddress)
        {
            if (IsDeleted) return Result.Failure(new DeletedPersonError("Deleted person can not be updated."));

            Name = name;
          
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;

            return Result.Success();
        }

        /// <summary>
        /// Deletes a person.
        /// </summary>
        public void Delete()
        {
            IsDeleted = true;
        }

    }

}