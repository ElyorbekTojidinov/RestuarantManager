using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleRepository _repository;
   // private readonly ILogger _logger;
    public UserRoleController(IUserRoleRepository repository)
    {
        _repository = repository;
       // _logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] UserRole userRole)
    {
       // _logger.LogInformation($"created {userRole}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _repository.CreateAsync(userRole);
            if (IsSuccess)
            {
                return Ok(userRole);
            }
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<IActionResult> GetById(int id)
    {
       // _logger.LogInformation($"UserRole Id {id}");
        UserRole? userRole = await _repository.GetAsync(x => x.UserRoleId == id);
        if (userRole != null)
        {
            return Ok(userRole);
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll()
    {
       // _logger.LogInformation($"{nameof(GetAll)}");
        var userRoles = await _repository.GetAllAsync();
        if (userRoles != null)
        {
            return Ok(userRoles);
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UserRole userRole)
    {
       // _logger.LogInformation($"{nameof(Update)}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _repository.UpdateAsync(userRole);
            if (IsSuccess)
            {
                return Ok(userRole);
            }
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("[action]{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        //_logger.LogInformation($"{nameof(Delete)}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _repository.DeleteAsync(id);
            if (IsSuccess)
            {
                return Ok();
            }
        }
        return BadRequest();
    }
}