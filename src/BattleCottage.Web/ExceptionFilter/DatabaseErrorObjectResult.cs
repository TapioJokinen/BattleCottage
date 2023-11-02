using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.ExceptionFilter
{
    public class DatabaseErrorObjectResult : ObjectResult
    {
        public DatabaseErrorObjectResult(string message)
            : base(new MessageResponse(message))
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
