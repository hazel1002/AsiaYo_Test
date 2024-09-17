using AsiaYo_Test.Models;
using AsiaYo_Test.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrder _order;

        public OrderController(IOrder order)
        {
            this._order = order;
        }

        [Route("orders")]
        [HttpPost]
        public async Task<IActionResult> ValidateOrder([FromBody] OrderReq req)
        {
            return await _order.ValidateOrder(req);
        }
    }
}
