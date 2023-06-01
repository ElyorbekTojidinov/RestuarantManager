using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController, Authorize]
public class WaiterController : Controller
{
    private readonly IWaiterRepository _waiterService;
    public WaiterController(IWaiterRepository waiterService)
    {
        _waiterService = waiterService;
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    
    public async Task<IActionResult> GetByIdWaiter(int id)
    {
        try
        {
            Waiter waiter = await _waiterService.GetByIdAsync(id);
            return Ok(new Response<Waiter> { Result = waiter });

        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Waiter>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false
            });
        }
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<IActionResult> GetAllWaiter(int page = 1, int pageSize = 10)
    {
        try
        {
            IQueryable<Waiter> Waiters = await _waiterService.GetAllAsync();

            var orders = Waiters.OrderBy(x => x.waiterId).Skip((page - 1) * pageSize).Take(pageSize);

            Response<Waiter> res = new()
            {
                Result = orders,
                pageSize = pageSize,
                page = page
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Waiter>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false,
            });
        }

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateWaiter([FromBody] Waiter waiter)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _waiterService.CreateAsync(waiter);
            if (IsSuccess)
            {
                return Ok(waiter);
            }
        }
        return BadRequest();
    }


    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateWaiter([FromBody] Waiter waiter)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _waiterService.UpdateAsync(waiter);
                if (isSuccess)
                    return Ok(new Response<Waiter>()
                    {
                        Result = waiter,
                    });
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(new Response<Waiter>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });

        }
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteWaiter([FromQuery] int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _waiterService.DeleteAsync(id);
                if (isSuccess) return Ok(new Response<Waiter>()
                {
                    Message = "Deleted success",
                    IsSuccess = true,
                    StatusCode = 200
                });

            }
            return BadRequest(new Response<Waiter>()
            {
                Message = "Not deleted",
                IsSuccess = false,
                StatusCode = 400
            });
        }
        catch (Exception e)
        {

            return BadRequest(new Response<Waiter>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });
        }
    }

}
