using Microsoft.AspNetCore.Mvc;
using Shopping_Api.Domain;
using Shopping_Api.Domain.ShoppingCartAggregate;
using Shopping_Api.Models;
using Shopping_Api.Specifications;
using Zero.SeedWorks;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly IRepository<Cart> _CartRepository;
        private readonly IRepository<Customer> _CustomerRepository;

        private readonly IRepository<Product> _ProductRepository;


        public CartController(IRepository<Cart> ShoppingCartRepository, IRepository<Customer> CustomerRepository, IRepository<Product> ProductRepository)
        {
            _CartRepository = ShoppingCartRepository;
            _CustomerRepository = CustomerRepository;
            _ProductRepository = ProductRepository;

        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(CartRequest model, CancellationToken cancellationToken)
        {

          

            if (ModelState.IsValid)
            {

                bool isValid = true;

                var product = await _ProductRepository.GetByIdAsync(model.ProductId);

                var customer = await _CustomerRepository.GetByIdAsync(model.CustomerId);

                var quantity = Quantity.Create(model.Quantity!);
                if (quantity.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), quantity.Error.Message);
                    isValid = false;
                }

              

                if (product == null || customer == null)
                {
                    return NotFound("Customer or Product Id Is Not Valid");
                }
                

                if (product.Quantity <= quantity.Value)
                {
                    return BadRequest("Quantity Is Not Valid");
                }




                if (isValid)
                {



                    var x = product.Quantity - quantity.Value;
                    await _ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


                    var cart = new Cart(model.CustomerId, model.ProductId, product.ProductName.Value , quantity.Value ,(int) product.Price.Value * quantity.Value);
                    product.Update((Quantity)x);

                    await _CartRepository.AddAsync(cart);

                    await _CartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok(cart.CustomerId);

                }
            }
            
            return ValidationProblem(ModelState);
        }



        

        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]

        [HttpGet("GetAllCartList")]
        public async Task<IActionResult> GetAllCartList()
        {
            var cart = await _CartRepository.ListAllAsync();

            return Ok(cart.Select(m => new CartResponse
            {
                //CartId = m.CartId,

                CustomerId = m.CustomerId,
                ProductId = m.ProductId,
                ProductName  = m.ProductName,  
                Quantity = m.Quantity,
                Price = m.Price    
                
                           

            }));
        }

        
        [HttpGet("GetCustomerById/{id}")]

        public async Task<IActionResult> GetCustomerById(int id)
        {
            var persons = await _CartRepository.ListAsync(new CartByCustomerIdSpecification(id));


            return Ok(persons.Select(m => new CartResponse
            {
               // CartId =m.CartId,
                CustomerId = m.CustomerId,
                ProductId = m.ProductId,
                ProductName=m.ProductName,  
                Quantity = m.Quantity,
                Price=m.Price   
                

            }));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _CartRepository.GetByIdAsync(id);
            var cartId = cart.ProductId;

            if (cart == null) return NotFound();

            //var book = new Books() { Id = cart.CartId };

            _CartRepository.Delete(cart);
            await _CartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Ok(cartId);
        }

    }
}
