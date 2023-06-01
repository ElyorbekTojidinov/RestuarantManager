using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.ProductController.ProductController;

[Route("api/[controller]")]
[ApiController]//, Authorize]

public class OrderController : Controller
{
    private readonly IOrdersRepository _orderService;
   
    public OrderController(IOrdersRepository orderService)
    {
        _orderService = orderService;
       
    }


    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<IActionResult> GetByIdOrder(int id)
    {
        try
        {
            Orders order = await _orderService.GetByIdAsync(id);
            return Ok(new Response<Orders> { Result = order });

        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Orders>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false
            });
        }
    }

    [HttpPost]
    [Route("[action]")]//, Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateOrder([FromBody] Orders order)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _orderService.CreateAsync(order);
            if (IsSuccess)
            {
                return Ok(order);
            }
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]")]//, Authorize(Roles = "GetAll")]
    public async Task<IActionResult> GetAllOrders(int page = 1, int pageSize = 10)
    {
        try
        {
            IQueryable<Orders> Orders = await _orderService.GetAllAsync();

            var orders = Orders.OrderBy(x => x.OrderId).Skip((page - 1) * pageSize).Take(pageSize);

            Response<Orders> res = new()
            {
                Result = orders,
                pageSize = pageSize,
                page = page
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Orders>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false,
            });
        }

    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateOrder([FromBody] Orders order)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _orderService.UpdateAsync(order);
                if (isSuccess)
                    return Ok(new Response<Orders>()
                    {
                        Result = order,
                    });
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(new Response<Orders>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });

        }
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteOrder([FromQuery] int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _orderService.DeleteAsync(id);
                if (isSuccess) return Ok(new Response<Orders>()
                {
                    Message = "Deleted success",
                    IsSuccess = true,
                    StatusCode = 200
                });

            }
            return BadRequest(new Response<Orders>()
            {
                Message = "Not deleted",
                IsSuccess = false,
                StatusCode = 400
            });
        }
        catch (Exception e)
        {

            return BadRequest(new Response<Orders>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });
        }
    }

}
