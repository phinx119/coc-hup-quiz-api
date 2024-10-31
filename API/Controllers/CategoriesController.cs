using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        CocHupQuizDBContext context = new CocHupQuizDBContext();

        // GET: Get category list
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var categories = await context.Categories.ToListAsync();

            if (categories.Count == 0)
            {
                return NotFound("Can not found any category.");
            }

            return Ok(categories);
        }
    }
}