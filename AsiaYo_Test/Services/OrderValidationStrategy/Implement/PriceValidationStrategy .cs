using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Services.OrderValidationStrategy.Implement
{
    public class PriceValidationStrategy : IOrderValidationStrategy
    {
        public IActionResult Validate(OrderReq order)
        {
            if (order.Price > 2000)
            {
                return new BadRequestObjectResult(new { error = "Price is over 2000" });
            }
            return null;
        }
    }
}
