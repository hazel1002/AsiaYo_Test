using AsiaYo_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsiaYo_Test.Services.Order.Implement
{
    public class Order : IOrder
    {
        public async Task<IActionResult> ValidateOrder(OrderReq req)
        {
            var res = new ResponseBase<OrderReq>
            {
                Entries = req
            };
            try
            {
                // 檢查 name
                if (!System.Text.RegularExpressions.Regex.IsMatch(req.Name, @"^[a-zA-Z\s]+$"))
                {
                    return new BadRequestObjectResult(new { error = "Name contains non-English characters" });
                }
                if (req.Name.Split(' ').Any(word => !char.IsUpper(word[0])))
                {
                    return new BadRequestObjectResult(new { error = "Name is not Capitalized" });
                }

                // 檢查 price
                if (req.Price > 2000)
                {
                    return new BadRequestObjectResult(new { error = "Price is over 2000" });
                }

                // 檢查 currency
                if (req.Currency != "TWD" && req.Currency != "USD")
                {
                    return new BadRequestObjectResult(new { error = "Currency format is wrong" });
                }
                if (req.Currency == "USD")
                {
                    req.Price *= 31;
                    req.Currency = "TWD";
                }
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new OkObjectResult(res);
        }
    }
}
