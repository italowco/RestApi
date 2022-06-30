using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Domain.Model;
using RestApi.Domain.TO;
using RestApi.Infraestructure.Data;

namespace RestApi.Application.Controllers
{
    
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IBus _bus;

        public ProductController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(p => p.Category).ToListAsync();

            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product product)
        {

            if (ModelState.IsValid)
            {
                
                try
                {
                    context.Add(product);
                    await context.SaveChangesAsync();
                    return product;

                } 
                catch
                {
                    return StatusCode(StatusCodes.Status404NotFound, new RestApi.Domain.TO.Response { Status = "Not Found", Message = "Catégoria nao existe" });
                }


            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            return products;
        }

        [HttpPost]
        [Route("bucket/{id:int}")]
        public async Task<IActionResult> AddProductToQueue(int id, [FromServices] DataContext context)
        {
            var product = await context.Products.Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                Uri uri = new Uri("rabbitmq://localhost:5672/productQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(product);
                
                return Ok();
            }

            return NotFound();
        }


    }
}
