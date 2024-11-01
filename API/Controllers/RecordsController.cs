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
        [HttpGet("GetListByCategoryAndDifficulty")]
        public async Task<IActionResult> GetListByCategoryAndDifficulty(string categoryName, string difficultyName)
        {
            var rank = 0;

            var recordList = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    User = r.User,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var records = await recordList
                .Where(r =>
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).OrderByDescending(r => r.HighScore)
                .ToListAsync();

            if (records.Count == 0)
            {
                return NotFound("Can not found any record.");
            }

            return Ok(records);
        }

        // GET: Get record list by user id
        [HttpGet("GetListByUserId")]
        public async Task<IActionResult> GetListByUserId(int userId)
        {
            var recordList = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    User = r.User,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var records = await recordList
                .Where(r => r.User.UserId == userId)
                .OrderByDescending(r => r.RecordDate)
                .ToListAsync();

            if (records.Count == 0)
            {
                return NotFound("Can not found any record.");
            }

            return Ok(records);
        }

        // GET: Get record by user id, category name, and difficulty name
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(int userId, string categoryName, string difficultyName)
        {
            var records = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    User = r.User,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var record = await records
                .Where(r =>
                    r.User.UserId == userId &&
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound("Can not found record.");
            }

            return Ok(record);
        }

        // POST: Create a new record
        [HttpPost]
        public async Task<IActionResult> Create(int userId, string categoryName, string difficultyName, int highScore)
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
        public async Task<IActionResult> Update(int? userId, string? categoryName, string? difficultyName, int? highScore)
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

            var records = context.Records
                .Select(r => new
                {
                    RecordId = r.RecordId,
                    User = r.User,
                    Category = r.Category,
                    Difficulty = r.Difficulty,
                    HighScore = r.HighScore,
                    RecordDate = r.RecordDate
                });

            var updatedRecord = await records
                .Where(r =>
                    r.User.UserId == userId &&
                    r.Category.CategoryName.Equals(categoryName) &&
                    r.Difficulty.DifficultyName.Equals(difficultyName)
                ).FirstOrDefaultAsync();

            return Ok(updatedRecord);
        }
    }
}
