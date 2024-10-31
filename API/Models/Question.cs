using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string IncorrectAnswers { get; set; } = null!;

        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual Difficulty? Difficulty { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
