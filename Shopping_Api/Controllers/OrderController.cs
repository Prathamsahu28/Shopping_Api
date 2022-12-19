using Microsoft.AspNetCore.Mvc;
using Shopping_Api.Domain;
using Shopping_Api.Domain.OrderAggregate;
using Shopping_Api.Domain.ShoppingCartAggregate;
using Shopping_Api.Models.OrderModel;
using Shopping_Api.Specifications;
using Zero.SeedWorks;
using Zero.Shopping_Api.Domain;
using Zero.Shopping_Api.Domain.ProductAggregate;

namespace Shopping_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IRepository<Cart> _CartRepository;
        private readonly IRepository<Customer> _CustomerRepository;

        private readonly IRepository<Product> _ProductRepository;

        private readonly IRepository<Order> _OrderRepository;
       // private readonly IRepository<OrderItem> _OrderItemRepository;


       

        public OrderController(IRepository<Cart> ShoppingCartRepository, IRepository<Customer> CustomerRepository, IRepository<Product> ProductRepository, IRepository<Order> OrderRepository )
        {
            _CartRepository = ShoppingCartRepository;
            _CustomerRepository = CustomerRepository;
            _ProductRepository = ProductRepository;
            _OrderRepository = OrderRepository;
           //  _OrderItemRepository = _OrderItemRepository;

        }

       [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("OrderFromCart/CustomerId")]
        public async Task<IActionResult> PostAsync(int customerId, CancellationToken cancellationToken)
        {
            var cartItem = await _CartRepository.ListAsync(new CartByCustomerIdSpecification(customerId));

            if (cartItem == null) { return NotFound("Customer Id is not valid"); }

            var totalProducts = (from emp in cartItem
                                 select emp).Sum(e => e.Quantity);

            //var product = await _ProductRepository.GetByIdAsync(cartItem.select(x => x.prp));
            var totalPrice = (from emp in cartItem select emp).Sum(e => e.Price);
           // int numberOfProducts = cartItem.Count();
           
            List<( int productId, string productName, Quantity quantity, int totalPrice) > 
                list = cartItem.Select(x => (x.ProductId, x.ProductName ,x.Quantity,x.Price )).ToList();
           
            DateTime currentDateTime = DateTime.Now;

            if(list.Count == 0) { return NotFound("Customer Cart IS Empty"); }

            var order = new Order(customerId, totalProducts, totalPrice, currentDateTime, list);
            await _OrderRepository.AddAsync(order);

           // List<int> list1 = cartItem.Select(x => x.ProductId).ToList();

            //List<(int c ,int product )> list = cartItem.Select(x => (x.ProductId, x.ProductName, x.Quantity, x.Price)).ToList();

            foreach (var item in cartItem)
            {
                // var cart = await _CartRepository.GetByIdAsync(item.CustomerId, item.ProductId);
                //var a = customerId + item;
                _CartRepository.Delete(item);
                await _CartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


            }


            await _OrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Ok(order.OrderId);

            /*
             * 
             * // List<(int productId, Quantity quantity)> orderProducts;
            var productId = from s in cartItem
                                select s.ProductId;

             var quantity = from s in cartItem
                             select s.Quantity;
             var list = new List<Tuple<int, Quantity>>();

             foreach (var item in productId)
             {
                 list.Add(new Tuple<int, Quantity>())

             }*/

            // List<(int productId, Quantity quantity)> orderProducts;


            //var tupleList = new List<(int, string)>
            //{
            //  (1, "cow1"),
            //  (5, "chickens1"),
            //  (1, "airplane1")
            //};

            // var items = from p in cartItem
            //  select (
            //new List<(int , Quantity)> (p.ProductId, p.Quantity));

            //var p = cartItem.ProductId;

            //Quantity q = cartItem.Quantity;

            //var tupleList = new List<(Product product, Quantity quantity)>;// { Tuple.Create(cartItem, q) };

            //tupleList.Add((product, quantity) =>

            // var tupleList = new List<Tuple<Product product, Quantity quantity>>
            //{
            //Tuple.Create( cartItem,q )
            // };
            //var items = new List<(Product product, Quantity quantity)> { ( q,cartItem)) };
            // tupleList.Add((cartItem, q) =>

            //tupleList.Add(new Tuple<int, string, string>(1, "Germany", "83 Millionen"));

            //var listCountryPopulation = new List<Tuple<Product product, Quantity quantity>>();
            //listCountryPopulation.Add(new Tuple<Product product, Quantity quantity>(cartItem, cartItem.Quantity));

            // foreach (var item in items)
            //{
            //  Console.WriteLine(item);    
            //}
        }

        [HttpGet("GetByOrderId/{id}")]

        public async Task<IActionResult> GetAllOrderById(int id)
        {
            var orderItem = await _OrderRepository.GetByIdAsync(id);

            if(orderItem == null) { return NotFound(); }
            //var cartItem = await _CartRepository.ListAsync(new CartByCustomerIdSpecification(orderItem.Select(s=> s.CustomerId));
            //var totalPrice = (from emp in orderItem select emp).Sum(e => e.);
            /// var x =  new OrderResponse() { }
            /// 
            return Ok( new OrderResponse
            {
                // CartId =m.CartId,
                OrderId = orderItem.OrderId,
                OrderDateTime = orderItem.OrderDate,
                CustomerId = orderItem.CustomerId,
                TotalNumberOfItems = orderItem.NumberOfItems,
                TotalPrice = orderItem.TotalPrice
                
      
        //OrderProducts = (List<OrderItem>)orderItem.OrderProducts
        //Quantity = orderItem.OrderProducts.Single().Quantity,   
        //Price = orderItem.OrderProducts.GetEnumerator().Current.TotalPrice,
        //TotalAmount =  (from emp in orderItem.OrderProducts select emp).Sum(e => e.TotalPrice)
        //ProductId = m.ProductId,
        //ProductName = cartItem.Select(x => (x.ProductId),
        //Quantity = m.Quantity,
        //Price = m.Price


    }) ;
        }

       



        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]

        [HttpGet("GetAllOrdersList")]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _OrderRepository.ListAllAsync();

            return Ok(order.Select(m => new OrderResponse
            {
                //CartId = m.CartId,

                CustomerId = m.CustomerId,
                OrderId = m.OrderId,
                OrderDateTime = m.OrderDate,
                TotalNumberOfItems = m.NumberOfItems,
                TotalPrice = m.TotalPrice
                // ProductName = m.OrderProducts.GetEnumerator().Current.ProductName,
                //Quantity = m.OrderProducts.GetEnumerator().Current.Quantity,   
                //Price = m.OrderProducts.GetEnumerator().Current.TotalPrice,
                //TotalAmount =  (from emp in m.OrderProducts select emp).Sum(e => e.TotalPrice)

                //OrderProducts =m.OrderProducts



            }));
        }

    }
}
