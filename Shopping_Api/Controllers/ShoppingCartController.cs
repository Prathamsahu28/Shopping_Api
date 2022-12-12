using Microsoft.AspNetCore.Mvc;
using Shopping_Api.Domain.ShoppingCartAggregate;
using Shopping_Api.Models;


using Zero.SeedWorks;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ItemAggregate;

namespace Shopping_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public readonly IRepository<ShoppingCart> _ShoppingCartRepository;
        private readonly IRepository<Customer> _CustomerRepository;

        private readonly IRepository<Item> _ItemRepository;


        public ShoppingCartController(IRepository<ShoppingCart>  ShoppingCartRepository , IRepository<Customer> CustomerRepository, IRepository<Item> ItemRepository)
        {
            _ShoppingCartRepository = ShoppingCartRepository;
            _CustomerRepository = CustomerRepository;
            _ItemRepository = ItemRepository;

        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(ShoppingCartRequest model, CancellationToken cancellationToken)
        {

            var item = await _ItemRepository.GetByIdAsync(model.ItemId);

            var customer = await _CustomerRepository.GetByIdAsync(model.CustomerId);


            if (item == null || customer == null)
            {
                return NotFound();
            }
            if(item.Quantity <= model.Quantity)
            {
                return BadRequest();
            }
            else
            {
                item.Update(item.Quantity-model.Quantity);
                await _ItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                int pri = item.Price * model.Quantity;
                var cart = new ShoppingCart(model.CustomerId, model.ItemId, item.ItemName, model.Quantity,pri);
               
                await _ShoppingCartRepository.AddAsync(cart);

                await _ShoppingCartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Ok(cart.CartId);




            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {

            var cart = await _ShoppingCartRepository.GetByIdAsync(id);

            if (cart == null) return NotFound();

            return Ok(new ShoppingCartResponse
            {

                CartId = cart.CartId,

                ItemId = cart.ItemId,
                Quantity = cart.Quantity,
              

            });
        }


        [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]

        [HttpGet]
        public async Task<IActionResult> GetAllActivePerson()
        {
            var cart = await _ShoppingCartRepository.ListAllAsync();

            return Ok(cart.Select(m => new ShoppingCartResponse
            {
                CartId = m.CartId,
                ItemId = m.ItemId,
                ItemName = m.ItemName,  
                Quantity = m.Quantity,
                Price = m.Price
                           

            }));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _ShoppingCartRepository.GetByIdAsync(id);

           if (cart == null) return NotFound();

            //var book = new Books() { Id = cart.CartId };
            
             _ShoppingCartRepository.Delete(cart);
            await _ShoppingCartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
          return NoContent();
        }

    }
}
