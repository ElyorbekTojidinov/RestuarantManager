using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace RestuarantManager.Filter;

public class AuthorizationFilter1Attribute : Attribute, IAuthorizationFilter, IAsyncAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {

    }
    public string Name { get; set; }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        if (hasClaim is null || !Name.Contains(hasClaim)) throw new Exception();
        return Task.CompletedTask;
    }
}
