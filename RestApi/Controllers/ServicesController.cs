using Microsoft.AspNetCore.Mvc;
using RestApi.Domain.TO;
using RestApi.Service.Interfaces;

namespace RestApi.Application.Controllers
{

    [ApiController]
    [Route("v1/services")]
    public class ServicesController : Controller
    {
        [HttpGet]
        [Route("singleton")]
        public IActionResult GetSingleton([FromServices] ISingletonService singletonService)
        {
            var time = DateTime.Now;
            
            return Ok(new Response { Status = time.ToString(), Message = singletonService.GetInfo() });
        }

        [HttpGet]
        [Route("scoped")]
        public IActionResult GetScoped([FromServices] IScopedService scopedService)
        {
            var time = DateTime.Now;

            return Ok(new Response { Status = time.ToString(), Message = scopedService.GetInfo() });
        }

        [HttpGet]
        [Route("transient")]
        public IActionResult GetTransient([FromServices] ITransientService transientService)
        {
            var time = DateTime.Now;

            return Ok(new Response { Status = time.ToString(), Message = transientService.GetInfo() });
        }
    }
}
