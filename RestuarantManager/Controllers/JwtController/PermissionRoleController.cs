using Aplication.Interfaces.InterfacesJWT;
using Aplication.Interfaces.InterfacesProducts;
using Domain.ModelsJWT;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class PermissionRoleController : Controller
{
    private readonly IPermissionRoleRepository _permissonRoleService;
  //  private readonly ILogger _logger;
    public PermissionRoleController(IPermissionRoleRepository permissonRoleRepository)
    {
        _permissonRoleService = permissonRoleRepository;
       // _logger = logger;
    }

    [HttpGet("RolePermissions")]
    public async Task<IActionResult> GetAllRolePermissionsAsync()
    {
        var rolePermissions = await _permissonRoleService.GetAllAsync();
        return Ok(rolePermissions);
    }


    [HttpPost("AddRolePermission")]
    public async Task<IActionResult> CreateRolePermissionAsync([FromForm] RolePermission rolePermission)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _permissonRoleService.CreateAsync(rolePermission);
        return Ok(rolePermission);
    }
    [HttpPut("UpdateRolePermission")]
    public async Task<IActionResult> UpdateRolePermissionAsync([FromForm] RolePermission rolePermission)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var IsAdded = await _permissonRoleService.UpdateAsync(rolePermission);
        if (IsAdded) return Ok(rolePermission);
        return BadRequest();

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRolePermissionAsync(int id)
    {
        var isDeleted = await _permissonRoleService.DeleteAsync(id);
        if (isDeleted)
            return Ok();
        return NotFound();
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<IActionResult> GetById(int id)
    {
       // _logger.LogInformation($"{nameof(GetById)} Id {id}");
        RolePermission? permission = await _permissonRoleService.GetAsync(x => x.PermissionId == id);
        if (permission != null)
        {
            return Ok(permission);
        }
        return BadRequest();
    }

    [HttpPost("AddRolePermissions")]
    public async Task<IActionResult> CreateRolePermissionsAsync([FromBody] IEnumerable<RolePermission> rolePermissions)
    {
        List<IActionResult> result = new List<IActionResult>();
        foreach (var role in rolePermissions)
        {
            result.Add(await CreateRolePermissionAsync(role));
        }
        return Ok(result);
    }


}
