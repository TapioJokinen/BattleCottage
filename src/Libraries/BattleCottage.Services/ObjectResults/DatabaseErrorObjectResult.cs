using BattleCottage.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Services.ObjectResults
{
    public class DatabaseErrorObjectResult : ObjectResult
    {
        public DatabaseErrorObjectResult(string message) : base(new MessageResponse(message))
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}