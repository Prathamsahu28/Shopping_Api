using Microsoft.AspNetCore.Mvc;
using Zero.Shopping_Api.Domain.ItemAggregate;
using Zero.Shopping_Api.Models;
using Zero.SeedWorks;


namespace Zero.Shopping_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Item> _ItemRepository;

        public ItemController(IRepository<Item> ItemRepository)
        {
            this._ItemRepository = ItemRepository;

        }


        //[ProducesResponseType(typeof(OrderResponseModel), StatusCodes.Status200OK)]

        //[HttpGet("/api/orders")]
        //public async Task<IActionResult> GetAllActivePerson()
        //{
        //    var persons = await _orderRepository.ListAsync(new ActivePersonSpecification());

        //    return Ok(persons.Select(m => new PersonResponseModel
        //    {
        //        EmailAddress = m.EmailAddress?.Value,
        //        MobileNumber = m.MobileNumber?.Value,
        //        Id = m.Id,
        //        Name = m.Name,
        //        TypeId = m.TypeId,
        //        TypeName = m.Type.Name
        //    }));
        //}

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(ItemRequestModel model, CancellationToken cancellationToken)
        {
        
              
                    var item = new Item( model.ItemName,model.Quantity, model.Price);

                    await _ItemRepository.AddAsync(item);

                    await _ItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok(item.ItemId);
                
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {

            var item = await _ItemRepository.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(new ItemResponseModel
            {
             
                ItemId = item.ItemId,
           
                ItemName = item.ItemName ,
                Quantity = item.Quantity,
                Price = item.Price  
                
            });
        }

        [ProducesResponseType(typeof(ItemResponseModel), StatusCodes.Status200OK)]

        [HttpGet]
        public async Task<IActionResult> GetAllActivePerson()
        {
            var item = await _ItemRepository.ListAllAsync();

            return Ok(item.Select(m => new ItemResponseModel
            {
                ItemId = m.ItemId,
                ItemName = m.ItemName,
                Quantity = m.Quantity,
                Price = m.Price

            }));
        }
    }
}
