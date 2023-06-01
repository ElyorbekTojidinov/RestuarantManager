using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.ProductController.ProductController;


[Route("api/[controller]")]
[ApiController, Authorize]
public class OrderProductController : Controller
{
    private readonly IOrderProductRepository _orderProductService;
   
    public OrderProductController(IOrderProductRepository orderProductService)
    {
        _orderProductService = orderProductService;
       
    }


    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<IActionResult> GetByIdOrderProduct(int id)
    {
        try
        {
            OrderProduct orderProduct = await _orderProductService.GetByIdAsync(id);
            return Ok(new Response<OrderProduct> { Result = orderProduct });

        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<OrderProduct>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false
            });
        }
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<IActionResult> GetAllOrders(int page = 1, int pageSize = 10)
    {
        try
        {
            IQueryable<OrderProduct> OrderProduct = await _orderProductService.GetAllAsync();

            var orderProduct = OrderProduct.OrderBy(x => x.OrderId).Skip((page - 1) * pageSize).Take(pageSize);

            Response<OrderProduct> res = new()
            {
                Result = orderProduct,
                pageSize = pageSize,
                page = page
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<OrderProduct>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false,
            });
        }

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateOrderProduct([FromBody] OrderProduct orderProduct)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _orderProductService.CreateAsync(orderProduct);
            if (IsSuccess)
            {
                return Ok(orderProduct);
            }
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateOrderProduct([FromBody] OrderProduct order)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _orderProductService.UpdateAsync(order);
                if (isSuccess)
                    return Ok(new Response<OrderProduct>()
                    {
                        Result = order,
                    });
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(new Response<OrderProduct>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });

        }
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteOrderProduct([FromQuery] int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _orderProductService.DeleteAsync(id);
                if (isSuccess) return Ok(new Response<OrderProduct>()
                {
                    Message = "Deleted success",
                    IsSuccess = true,
                    StatusCode = 200
                });

            }
            return BadRequest(new Response<OrderProduct>()
            {
                Message = "Not deleted",
                IsSuccess = false,
                StatusCode = 400
            });
        }
        catch (Exception e)
        {

            return BadRequest(new Response<OrderProduct>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });
        }
    }

}
