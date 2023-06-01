using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController, Authorize]
public class WaiterOrderController : Controller
{
    private readonly IWaiterOrderRepository _waiterOrderService;
    public WaiterOrderController(IWaiterOrderRepository waiterOrderService)
    {
        _waiterOrderService = waiterOrderService;
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<IActionResult> GetByIdWaiterOrder(int id)
    {
        try
        {
            WaiterOrder waiterOrder = await _waiterOrderService.GetByIdAsync(id);
            return Ok(new Response<WaiterOrder> { Result = waiterOrder });

        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<WaiterOrder>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false
            });
        }
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<IActionResult> GetAllWaiterOrder(int page = 1, int pageSize = 10)
    {
        try
        {
            IQueryable<WaiterOrder> WaiterOrders = await _waiterOrderService.GetAllAsync();

            var orders = WaiterOrders.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

            Response<WaiterOrder> res = new()
            {
                Result = orders,
                pageSize = pageSize,
                page = page
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<WaiterOrder>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false,
            });
        }

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateWaiterOrder([FromBody] WaiterOrder waiterOrder)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _waiterOrderService.CreateAsync(waiterOrder);
            if (IsSuccess)
            {
                return Ok(waiterOrder);
            }
        }
        return BadRequest();
    }


    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateWaiterOrder([FromBody] WaiterOrder waiterOrder)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _waiterOrderService.UpdateAsync(waiterOrder);
                if (isSuccess)
                    return Ok(new Response<WaiterOrder>()
                    {
                        Result = waiterOrder,
                    });
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(new Response<WaiterOrder>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });

        }
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteWaiterOrder([FromQuery] int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _waiterOrderService.DeleteAsync(id);
                if (isSuccess) return Ok(new Response<WaiterOrder>()
                {
                    Message = "Deleted success",
                    IsSuccess = true,
                    StatusCode = 200
                });

            }
            return BadRequest(new Response<WaiterOrder>()
            {
                Message = "Not deleted",
                IsSuccess = false,
                StatusCode = 400
            });
        }
        catch (Exception e)
        {

            return BadRequest(new Response<WaiterOrder>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });
        }
    }

}

