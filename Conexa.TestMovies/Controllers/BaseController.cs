using Conexa.TestMovies.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Conexa.TestMovies.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ProcessResult(BaseResponse result)
        {
            IActionResult response;
            if(result.Result != null)
            {
                response = new ObjectResult(result.Result)
                {
                    StatusCode = result.Code
                };
            }
            else if(result.Errors?.Count > 0) 
            {
                response = new ObjectResult(result.Errors)
                {
                    StatusCode = result.Code
                };
            }
            else
            {
                response = new StatusCodeResult(result.Code);
            }
            return response;
        }
    }
}
