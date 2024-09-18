using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Services.OrderValidationStrategy
{
    public interface IOrderValidationStrategy
    {
        IActionResult Validate(OrderReq order);
    }
}
