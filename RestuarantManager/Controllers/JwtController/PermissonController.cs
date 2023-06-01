using Aplication.Interfaces.InterfacesJWT;
using Aplication.Interfaces.InterfacesProducts;
using Domain.ModelsJWT;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RestuarantManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class PermissonController : Controller
{
    private readonly IPermissionRepository _permissionService;
   // private readonly ILogger _logger;
    public PermissonController(IPermissionRepository permissionService)
    {
        _permissionService = permissionService;
       // _logger = logger;
    }

    [HttpGet("Permissions")]
    public async Task<IActionResult> GetAllPermissionsAsync()
    {
       // _logger.LogInformation($"{nameof(GetAllPermissionsAsync)}");
        var permissions = await _permissionService.GetAllAsync();
        if (permissions != null)
        {
            return Ok(permissions);
        }
        return BadRequest();
    }


    [HttpPost("AddPermission")]
    public async Task<IActionResult> CreatePermissionAsync([FromForm] Permission permission)
    {
       // _logger.LogInformation($"{nameof(CreatePermissionAsync)} {permission.PermissionName}");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _permissionService.CreateAsync(permission);
            if (IsSuccess)
            {
                return Ok(permission);
            }
        }
        
        return BadRequest(ModelState);
    }


    [HttpPut("UpdatePermission")]
    public async Task<IActionResult> UpdatePermissionAsync([FromForm] Permission permission)
    {
        if (ModelState.IsValid)
        {
            var IsAdded = await _permissionService.UpdateAsync(permission);
            if (IsAdded) return Ok(permission);
           
        }
        
        return BadRequest();

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePermissionAsync(int id)
    {
       // _logger.LogInformation($"{nameof(DeletePermissionAsync)}{id} deleted");
        if (ModelState.IsValid)
        {
            bool IsSuccess = await _permissionService.DeleteAsync(id);
            if (IsSuccess)
            {
                return Ok();
            }
        }
        return BadRequest();
    }


    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdPermissionAsync(int id)
    {
        //_logger.LogInformation($"{nameof(GetByIdPermissionAsync)} Id {id}");
        Permission? permission = await _permissionService.GetAsync(x => x.PermissionId == id);
        if (permission != null)
        {
            return Ok(permission);
        }
        return BadRequest();
    }


    [HttpPost("AddPermissions")]
    public async Task<IActionResult> CreatePermissionsAsync([FromBody] IEnumerable<Permission> permissions)
    {
        List<IActionResult> result = new List<IActionResult>();
        foreach (var permission in permissions)
        {
            result.Add(await CreatePermissionAsync(permission));
        }
        return Ok(result);
    }


}
