using BattleCottage.Core.Exceptions;
using BattleCottage.Services.ObjectResults;
using BattleCottage.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace BattleCottage.Services
{
    public class BaseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException ||
                context.Exception is RegisterException)
            {
                context.Result = new BadRequestObjectResult(new MessageResponse(context.Exception.Message));
            }
            else if (context.Exception is ObjectNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new MessageResponse(context.Exception.Message));
            }
            else if (context.Exception is TokenException || context.Exception is SecurityTokenException)
            {
                context.Result = new UnauthorizedObjectResult(new MessageResponse(context.Exception.Message));
            }
            else
            {
                context.Result = new InternalServerErrorObjectResult("An unknown error occurred.");
            }
        }
    }
}