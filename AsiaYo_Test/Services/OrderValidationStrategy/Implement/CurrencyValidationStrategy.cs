using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Services.OrderValidationStrategy.Implement
{
    public class CurrencyValidationStrategy : IOrderValidationStrategy
    {
        public IActionResult Validate(OrderReq order)
        {
            if (order.Currency != "TWD" && order.Currency != "USD")
            {
                return new BadRequestObjectResult(new { error = "Currency format is wrong" });
            }
            if (order.Currency == "USD")
            {
                order.Price *= 31;
                order.Currency = "TWD";
            }
            return null;
        }
    }
}
