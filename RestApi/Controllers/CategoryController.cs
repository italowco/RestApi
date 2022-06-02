using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Domain;
using RestApi.Infraestructure.Data;

namespace RestApi.Application.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return categories;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById([FromServices] DataContext context, int id)
        {
            Category category = await context.Categories.FirstOrDefaultAsync(c => c.id == id);

            if (category == null) return NotFound();

            return category;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteById([FromServices] DataContext context, int id)
        {
            Category category = await context.Categories.FirstOrDefaultAsync(c => c.id == id);

            if (category == null) return NotFound();

            context.Categories.Remove(category);
            var response = context.SaveChanges();

            return Ok();
        }
    }
}

