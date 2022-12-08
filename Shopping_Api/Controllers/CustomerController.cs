using Microsoft.AspNetCore.Mvc;
using Zero.SeedWorks;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Models;
using Zero.Shopping_Api.Specifications;

namespace Zero.AspNetCoreServiceProjectExample.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _personRepository;


        public CustomerController(IRepository<Customer> personRepository)
        {
            this._personRepository = personRepository;

        }

        /// <summary>
        /// Fetch the List Of Person 
        /// </summary>
        /// <response code="200">Person added!</response>
        [ProducesResponseType(typeof(CustomerResponseModel), StatusCodes.Status200OK)]
        
        [HttpGet("/api/persons")]
        public async Task<IActionResult> GetAllActivePerson()
        {
            var persons = await _personRepository.ListAsync(new ActivePersonSpecification());

            return Ok(persons.Select(m => new CustomerResponseModel
            {
                EmailAddress = m.EmailAddress?.Value,
                MobileNumber = m.MobileNumber?.Value,
                Id = m.CustomerId,
                Name = m.Name,
               
            }));
        }
        /// <summary>
        /// Fetch the Detail Of Person By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Get Selected Person Data</response>
        /// <response code="404">When the Person Data Is Deleted or Not Exist</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomerResponseModel), StatusCodes.Status200OK)]


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {

            var person = await _personRepository.GetByIdAsync(id);

            if (person == null) return NotFound();

            return Ok(new CustomerResponseModel
            {
                EmailAddress = person.EmailAddress?.Value,
                MobileNumber = person.MobileNumber?.Value,
                Id = person.CustomerId,
                Name = person.Name,
              
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Person added!</response>
        /// <response code="400">If there is any input validation error.</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(CustomerRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                var name = CustomerName.Create(model.Name!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Name), name.Error.Message);
                    isValid = false;
                }

                var mobilenumber = MobileNumber.Create(model.MobileNumber, true);
                if (mobilenumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.MobileNumber), mobilenumber.Error.Message);
                    isValid = false;
                }

                var emailAddress = EmailAddress.Create(model.EmailAddress, true);
                if (emailAddress.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.EmailAddress), emailAddress.Error.Message);
                    isValid = false;
                }

               

                if (isValid)
                {
                    var person = new Customer(name.Value, mobilenumber.Value, emailAddress.Value);

                    await _personRepository.AddAsync(person);

                    await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok(person.CustomerId);
                }
            }
            return ValidationProblem(ModelState);
        }
        /// <summary>
        /// Update the data of selected Person Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Updataed Data Of the Person of Seleted Id</response>
        /// <response code="400">If their is any input data Validation Error</response>
        /// <response code="404">Person Data are Deleted or Not Exist</response>
        [ProducesResponseType(typeof(CustomerRequestModel),StatusCodes.Status200OK)]  
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id, CustomerRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var person = await _personRepository.GetByIdAsync(id);

                if (person == null || person.IsDeleted) return NotFound();

                bool isValid = true;

                var name = CustomerName.Create(model.Name!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Name), name.Error.Message);
                    isValid = false;
                }

                var mobileNumber = MobileNumber.Create(model.MobileNumber, true);
                if (mobileNumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.MobileNumber), mobileNumber.Error.Message);
                    isValid = false;

                }

                var emailAddress = EmailAddress.Create(model.EmailAddress, true);
                if (emailAddress.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.EmailAddress), emailAddress.Error.Message);
                    isValid = false;
                }

              

                if (isValid)
                {

                    var result = person.Update(name.Value, mobileNumber.Value, emailAddress.Value);

                    if (result.IsSuccess)
                    {
                        await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                        return NoContent();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Error.Message);
                    }
                }

            }
            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Delete the  Person Data 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="404">Person Data are Not Exist</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var customer = await _personRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            customer.Delete();
            await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return NoContent();
        }
    }
}
