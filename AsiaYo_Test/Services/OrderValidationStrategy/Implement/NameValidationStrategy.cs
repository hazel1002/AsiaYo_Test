using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Services.OrderValidationStrategy.Implement
{
    public class NameValidationStrategy : IOrderValidationStrategy
    {
        public IActionResult Validate(OrderReq order)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(order.Name, @"^[a-zA-Z\s]+$"))
            {
                return new BadRequestObjectResult(new { error = "Name contains non-English characters" });
            }
            if (order.Name.Split(' ').Any(word => !char.IsUpper(word[0])))
            {
                return new BadRequestObjectResult(new { error = "Name is not Capitalized" });
            }
            return null;
        }
    }
}
