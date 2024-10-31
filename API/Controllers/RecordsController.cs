using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        CocHupQuizDBContext context = new CocHupQuizDBContext();

        // GET: Get record list
        [HttpGet("{categoryName}/{difficultyName}")]
        public async Task<IActionResult> GetList(string categoryName, string difficultyName)
        {
            var records = await context.Records
                .Where(r =>
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).ToListAsync();

            if (records.Count == 0)
            {
                return NotFound("Can not found any record.");
            }

            return Ok(records);
        }

        // GET: Get record list by user id
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetListByUserId(int userId)
        {
            var records = await context.Records.Where(r => r.UserId == userId).ToListAsync();

            if (records.Count == 0)
            {
                return NotFound("Can not found any record.");
            }

            return Ok(records);
        }

        // GET: Get record by user id, category name, and difficulty name
        [HttpGet("{userId}/{categoryName}/{difficultyName}")]
        public async Task<IActionResult> GetByUserId(int userId, string categoryName, string difficultyName)
        {
            var records = await context.Records
                .Where(r =>
                    r.UserId == userId &&
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).FirstOrDefaultAsync();

            if (records == null)
            {
                return NotFound("Can not found record.");
            }

            return Ok(records);
        }

        // POST: Create a new record
        [HttpPost]
        public async Task<IActionResult> CreateRecord(int userId, string categoryName, string difficultyName, int highScore)
        {
            // Get category by name
            var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName.Equals(categoryName));
            if (category == null)
            {
                return NotFound("Category does not exsist.");
            }

            // Get difficulty by name
            var difficulty = await context.Difficulties.FirstOrDefaultAsync(c => c.DifficultyName.Equals(difficultyName));
            if (difficulty == null)
            {
                return NotFound("Difficulty does not exsist.");
            }

            var newRecord = new Record
            {
                UserId = userId,
                CategoryId = category.CategoryId,
                DifficultyId = difficulty.DifficultyId,
                HighScore = highScore,
                RecordDate = DateTime.UtcNow
            };

            context.Records.Add(newRecord);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = newRecord.RecordId }, newRecord);
        }


        // PUT: Update an existing record
        [HttpPut]
        public async Task<IActionResult> UpdateRecord(int? userId, string? categoryName, string? difficultyName, int? highScore)
        {
            var existingRecord = await context.Records
                .Where(r =>
                    r.UserId == userId &&
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).FirstOrDefaultAsync();
            if (existingRecord == null)
            {
                return NotFound("Record not found.");
            }

            // Update fields if values are provided
            if (highScore.HasValue)
            {
                existingRecord.HighScore = highScore.Value;
            }
            existingRecord.RecordDate = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
