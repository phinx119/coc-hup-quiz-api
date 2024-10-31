using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        CocHupQuizDBContext context = new CocHupQuizDBContext();

        // GET: Get difficulty list
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var difficulties = await context.Difficulties.ToListAsync();

            if (difficulties.Count == 0)
            {
                return NotFound("Can not found any difficulty.");
            }

            return Ok(difficulties);
        }
    }
}