using Microsoft.AspNetCore.Mvc;
using RestApi.Domain.TO;
using RestApi.Service.Interfaces;

namespace RestApi.Application.Controllers
{

    [ApiController]
    [Route("v1/services")]
    public class ServicesController : Controller
    {
        public readonly IScopedService scopedService2;
        public readonly ITransientService transientService2;
        public readonly ISingletonService singletonService2;

        public ServicesController(IScopedService scopedService2, ITransientService transientService2, ISingletonService singletonService2)
        {
            this.scopedService2 = scopedService2;
            this.transientService2 = transientService2;
            this.singletonService2 = singletonService2;
        }

        [HttpGet]
        [Route("singleton")]
        public IActionResult GetSingleton([FromServices] ISingletonService singletonService)
        {
            var time = DateTime.Now;
            
            return Ok(new Response { Status = time.ToString(), Message = singletonService.GetInfo() + " -- " + singletonService2.GetInfo() });
        }

        [HttpGet]
        [Route("scoped")]
        public IActionResult GetScoped([FromServices] IScopedService scopedService)
        {
            var time = DateTime.Now;

            return Ok(new Response { Status = time.ToString(), Message = scopedService.GetInfo() + " -- " + scopedService2.GetInfo() });
        }

        [HttpGet]
        [Route("transient")]
        public IActionResult GetTransient([FromServices] ITransientService transientService)
        {
            var time = DateTime.Now;

            return Ok(new Response { Status = time.ToString(), Message = transientService.GetInfo() + " -- " + transientService2.GetInfo() });
        }
    }
}
