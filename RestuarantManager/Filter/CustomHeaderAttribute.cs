using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestuarantManager.Filter
{
    public class CustomHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public CustomHeaderAttribute(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = AddCustomHeader(objectResult.Value);
            }
            else if (context.Result is JsonResult jsonResult)
            {
                jsonResult.Value = AddCustomHeader(jsonResult.Value);
            }

            base.OnResultExecuting(context);
        }

        private object AddCustomHeader(object result)
        {
            if (result is Categories myViewModel)
            {
                myViewModel.CategoryName = _headerValue;
            }
            else if (result is Categories myDataModel)
            {
                myDataModel.CategoryName = _headerValue;
            }

            return result;
        }
    }

}

