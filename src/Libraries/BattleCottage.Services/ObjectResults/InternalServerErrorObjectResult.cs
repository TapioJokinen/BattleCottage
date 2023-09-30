using BattleCottage.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Services.ObjectResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(string message) : base(new MessageResponse(message))
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}