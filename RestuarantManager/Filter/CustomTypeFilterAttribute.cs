using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace RestuarantManager.Filter
{
    public class CustomTypeFilterAttribute : TypeFilterAttribute
    {
        public CustomTypeFilterAttribute(string claimTypes, string claimValue):base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimTypes, claimValue) };
        }

    }
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c=>c.Type== _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
