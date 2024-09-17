using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;
namespace AsiaYo_Test.Services.Order
{
    public interface IOrder
    {
        Task<IActionResult> ValidateOrder(OrderReq req);
    }
}
