using DAL.Entities;
using DAL.Repositories;
using ESTORE.Attributes;
using ESTORE.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTORE.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(IOrderRepository orderRepository, UserManager<IdentityUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager; 

        }

        /* [ServiceFilter(typeof(LoggerFilter))]*/

        [Description("Retreive Order by Order id")]
        [AllowAccess("Order:Read")]
        [HttpGet]
        public async Task< IActionResult> get(int id)
        {
            var result = await this._orderRepository.GetByIdAsync(id);
            if(result == null) {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> add([FromBody] Order order)
        {
            var user = await _userManager.FindByIdAsync(order.CustomerId);
            if(user == null)
            {
                return BadRequest("Customer not found");
            }
            order.Customer = user;
            await this._orderRepository.AddAsync(order);
            var result = await this._orderRepository.SaveAsync();
            if (result == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
