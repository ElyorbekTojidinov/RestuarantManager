using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.ProductControllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ProductController : Controller
{
    private readonly IProductRepository _productService;
    public ProductController(IProductRepository productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<IActionResult> GetByIdProduct(int id)
    {
        try
        {
            Products product = await _productService.GetByIdAsync(id);
            return Ok(new Response<Products> { Result = product });

        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Products>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false
            });
        }
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<IActionResult> GetAllProduct(int page = 1, int pageSize = 10)
    {
        try
        {
            IQueryable<Products> Products = await _productService.GetAllAsync();

            var orders = Products.OrderBy(x => x.ProductId).Skip((page - 1) * pageSize).Take(pageSize);

            Response<Products> res = new()
            {
                Result = orders,
                pageSize = pageSize,
                page = page
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<Products>()
            {
                Message = e.Message,
                StatusCode = 500,
                IsSuccess = false,
            });
        }

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateProduct([FromBody] Products product)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _productService.CreateAsync(product);
            if (IsSuccess)
            {
                return Ok(product);
            }
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateProduct([FromBody] Products product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _productService.UpdateAsync(product);
                if (isSuccess)
                    return Ok(new Response<Products>()
                    {
                        Result = product,
                    });
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(new Response<Products>()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 400
            });

        }
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteProduct([FromQuery] int id)
    {
        if (ModelState.IsValid)
        {
            bool isSuccess = await _productService.DeleteAsync(id);
            if (isSuccess) return Ok(new Response<Products>()
            {
                Message = "Deleted success",
                IsSuccess = true,
                StatusCode = 200
            });

        }
        return BadRequest(new Response<Products>()
        {
            Message = "Not deleted",
            IsSuccess = false,
            StatusCode = 400
        });

    }

}
