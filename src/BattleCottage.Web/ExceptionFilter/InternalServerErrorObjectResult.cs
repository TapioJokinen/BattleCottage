using BattleCottage.Web;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.ExceptionFilter
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(string message)
            : base(new MessageResponse(message))
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
