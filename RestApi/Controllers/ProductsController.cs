using FluentValidation.Results;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Domain.Model;
using RestApi.Domain.Model.Validators;
using RestApi.Domain.TO;
using RestApi.Infraestructure.Data;
using RestApi.Infraestructure.Repositories;
using Response = RestApi.Domain.TO.Response;

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
        public async Task<ActionResult<List<Product>>> Get([FromServices] IProductRepository repository)
        {
            var products = await repository.Get().Include(p => p.Category).ToListAsync();

            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] IProductRepository repository, int id)
        {
            var products = await repository.Get().Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] IProductRepository repository, [FromBody] Product product)
        {
            CreateProductValidator productValidator = new();
            var validatorResult = productValidator.Validate(product);
            
            if (validatorResult.IsValid)
            {
                
                //context.Add(product);
                await repository.AddAsync(product);
                return product;

            }
            else
            {
                
                List<string> ValidationMessages = new List<string>();

                foreach (ValidationFailure failure in validatorResult.Errors)
                {
                    ValidationMessages.Add(failure.ErrorMessage);
                }
                var response = new ResponseTO("Bad request", ValidationMessages);

                return BadRequest(response);
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
