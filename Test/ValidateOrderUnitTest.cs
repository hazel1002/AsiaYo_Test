using NUnit.Framework;
using AsiaYo_Test.Services.Order;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using AsiaYo_Test.Services.Order.Implement;
using AsiaYo_Test.Models;
using NUnit.Framework.Legacy;

[TestFixture]
public class OrderServiceTests
{
    private IOrder _order;

    [SetUp]
    public void Setup()
    {
        _order = new Order(); //DIª`¤J
    }

    [Test]
    public async Task ValidateOrder_NameContainsNonEnglishCharacters_ReturnsBadRequest()
    {
        var req = new OrderReq
        {
            Id = "A0000001",
            Name = "Melody °²¤é°s©±",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 1000,
            Currency = "TWD"
        };

        var result = await _order.ValidateOrder(req) as ObjectResult;
        ClassicAssert.AreEqual(400, result.StatusCode);
        ClassicAssert.AreEqual("Name contains non-English characters", result.Value);
    }

    [Test]
    public async Task ValidateOrder_NameIsNotCapitalized_ReturnsBadRequest()
    {
        var req = new OrderReq
        {
            Id = "A0000002",
            Name = "melody Holiday Inn",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 1000,
            Currency = "TWD"
        };

        var result = await _order.ValidateOrder(req) as ObjectResult;
        ClassicAssert.AreEqual(400, result.StatusCode);
        ClassicAssert.AreEqual("Name is not Capitalized", result.Value);
    }

    [Test]
    public async Task ValidateOrder_PriceOver2000_ReturnsBadRequest()
    {
        var req = new OrderReq
        {
            Id = "A0000003",
            Name = "Melody Holiday Inn",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 2050,
            Currency = "TWD"
        };

        var result = await _order.ValidateOrder(req) as ObjectResult;
        ClassicAssert.AreEqual(400, result.StatusCode);
        ClassicAssert.AreEqual("Price is over 2000", result.Value);
    }

    [Test]
    public async Task ValidateOrder_InvalidCurrency_ReturnsBadRequest()
    {
        var req = new OrderReq
        {
            Id = "A0000004",
            Name = "Melody Holiday Inn",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 1000,
            Currency = "JPY"
        };

        var result = await _order.ValidateOrder(req) as ObjectResult;
        ClassicAssert.AreEqual(400, result.StatusCode);
        ClassicAssert.AreEqual("Currency format is wrong", result.Value);
    }

    [Test]
    public async Task ValidateOrder_CurrencyUSD_TransformsToTWD()
    {
        var req = new OrderReq
        {
            Id = "A0000005",
            Name = "Melody Holiday Inn",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 100,
            Currency = "USD"
        };

        var result = await _order.ValidateOrder(req);
        ClassicAssert.AreEqual(3100, req.Price); // 100 * 31 = 3100
        ClassicAssert.AreEqual("TWD", req.Currency);
    }

    [Test]
    public async Task ValidateOrder_ValidOrder_ReturnsOk()
    {
        var req = new OrderReq
        {
            Id = "A0000006",
            Name = "Melody Holiday Inn",
            Address = new Address
            {
                City = "taipei-city",
                District = "da-an-district",
                Street = "fuxing-south-road"
            },
            Price = 1000,
            Currency = "TWD"
        };

        var result = await _order.ValidateOrder(req) as OkObjectResult;
        ClassicAssert.AreEqual(200, result.StatusCode);
    }
}
