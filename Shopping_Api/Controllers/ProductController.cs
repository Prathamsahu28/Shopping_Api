using Microsoft.AspNetCore.Mvc;
using Zero.Shopping_Api.Domain.ProductAggregate;
using Zero.Shopping_Api.Models;
using Zero.SeedWorks;
using Shopping_Api.Domain;
using Shopping_Api.Domain.ProductAggregate;

namespace Zero.Shopping_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _ProductRepository;

        public ProductController(IRepository<Product> ItemRepository)
        {
            this._ProductRepository = ItemRepository;
            



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
        public async Task<IActionResult> PostAsync(ProductRequestModel model, CancellationToken cancellationToken)
        {

            if (ModelState.IsValid)
            {
                bool isValid = true;

                var quantity = Quantity.Create(model.Quantity!);
                if (quantity.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), quantity.Error.Message);
                    isValid = false;
                }

                var price = Price.Create(model.Price!);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Price), price.Error.Message);
                    isValid = false;
                }
                var name = ProductName.Create(model.ProductName!);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Price), price.Error.Message);
                    isValid = false;
                }

                if (isValid)
                {
                    var Product = new Product(name.Value, quantity.Value, price.Value);

                    await _ProductRepository.AddAsync(Product);

                    await _ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok(Product.ProductId);
                }

            }
            return ValidationProblem(ModelState);
        
               
           
        }


        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]

        [HttpGet("GetAllProductsList")]
        public async Task<IActionResult> GetAllProducts()
        {
            var item = await _ProductRepository.ListAllAsync();

            return Ok(item.Select(m => new ProductResponseModel
            {
                ProductId = m.ProductId,
                ProductName = m.ProductName,
                Quantity = m.Quantity,
                Price = m.Price

            }));
        }


        [HttpGet("GetProductId/{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {

            var item = await _ProductRepository.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(new ProductResponseModel
            {

                ProductId = item.ProductId,

                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price  
                
            });
        }

      
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetByIdAsync(id);
            string i = product.ProductName;
            if (product == null) return NotFound();

            //var book = new Books() { Id = cart.CartId };

            _ProductRepository.Delete(product);
            await _ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Ok(i);
        }


       
    }
}
