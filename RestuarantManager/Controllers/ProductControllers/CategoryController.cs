using Aplication.Interfaces.InterfacesProducts;
using Aplication.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestuarantManager.Filter;

namespace RestuarantManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryService;
  
    public CategoryController(ICategoryRepository categoryServise)
    {
        _categoryService = categoryServise;
       
    }

    [HttpGet]
    [Route("[action]")]//, Authorize(Roles = "GetById")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {

        Categories category = await _categoryService.GetByIdAsync(id);
        return Ok(new Response<Categories> { Result = category });


    }

    [CustomHeader("X-Custom-Header", "MyCustomValue")]
    [AuthorizationFilter1(Name = "Elyorbek")]
    [HttpGet]
    [Route("[action]")]//, Authorize(Roles = "GetAll")]
    public async Task<ActionResult<Response<PaginatedList<Categories>>>> GetAllCategory(int page = 1, int pageSize = 10)
    {

        IQueryable<Categories> Category = await _categoryService.GetAllAsync();
        PaginatedList<Categories> categorys = await PaginatedList<Categories>.CreateAsync(Category, page, pageSize);

        Response<PaginatedList<Categories>> res = new()
        {
            Result = categorys
        };
        return Ok(res);


        

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    public async Task<IActionResult> CreateCategory([FromBody] Categories category)
    {
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _categoryService.CreateAsync(category);
            if (IsSuccess)
            {
                return Ok(category);
            }
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    public async Task<IActionResult> UpdateCategory([FromBody] Categories category)
    {
        if (ModelState.IsValid)
        {
            bool isSuccess = await _categoryService.UpdateAsync(category);
            if (isSuccess)
                return Ok(new Response<Categories>()
                {
                    Result = category,
                });
        }
        return BadRequest(new Response<Categories>()
        {
            Message = "Error",
            IsSuccess = false,
            StatusCode = 400
        });

    }


    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<IActionResult> DeleteCategory([FromQuery] int id)
    {

        if (ModelState.IsValid)
        {
            bool isSuccess = await _categoryService.DeleteAsync(id);
            if (isSuccess) return Ok(new Response<Categories>()
            {
                Message = "Deleted success",
                IsSuccess = true,
                StatusCode = 200
            });

        }
        return BadRequest(new Response<Categories>()
        {
            Message = "Not deleted",
            IsSuccess = false,
            StatusCode = 400
        });

    }

}
