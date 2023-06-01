using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _repository;
    //private readonly ILogger _logger;
    public RoleController(IRoleRepository repository)
    {
        _repository = repository;
        //_logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] Roles role)
    {
        //_logger.LogInformation($"{nameof(Create)} {role.Name}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _repository.CreateAsync(role);
            if (IsSuccess)
            {
                return Ok(role);
            }
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //_logger.LogInformation($"{nameof(GetById)}");
        Roles? role = await _repository.GetAsync(x => x.Id == id);
        if (role != null)
        {
            return Ok(role);
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll()
    {
       // _logger.LogInformation($"{GetAll}");
        var roles = await _repository.GetAllAsync();
        if (roles != null)
        {
            return Ok(roles);
        }
        return BadRequest();
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] Roles role)
    {
        //_logger.LogInformation($"{nameof(GetById)}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _repository.UpdateAsync(role);
            if (IsSuccess)
            {
                return Ok(role);
            }
        }
        return BadRequest();
    }

    [HttpDelete]
    [Route("[action]{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       // _logger.LogInformation($"{nameof(Delete)}{id}");
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
