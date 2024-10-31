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
            var recordList = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    UserId = r.UserId,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var records = await recordList
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
            var recordList = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    UserId = r.UserId,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var records = await recordList.Where(r => r.UserId == userId).ToListAsync();

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
            var recordList = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    UserId = r.UserId,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var records = await recordList
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
            // Get user by user id
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound("User does not exsist.");
            }

            // Get category by name
            var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName.Equals(categoryName));
            if (category == null)
            {
                return NotFound("Category does not exsist.");
            }

            // Get difficulty by name
            var difficulty = await context.Difficulties.FirstOrDefaultAsync(d => d.DifficultyName.Equals(difficultyName));
            if (difficulty == null)
            {
                return NotFound("Difficulty does not exsist.");
            }

            var newRecord = new Record
            {
                User = user,
                Category = category,
                Difficulty = difficulty,
                HighScore = highScore,
                RecordDate = DateTime.UtcNow
            };

            context.Records.Add(newRecord);
            await context.SaveChangesAsync();

            return Created("Record created successfully.", newRecord);
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

            return Ok(existingRecord);
        }
    }
}
