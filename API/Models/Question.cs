using System;
using System.Collections.Generic;

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

        public virtual Category? Category { get; set; }
        public virtual Difficulty? Difficulty { get; set; }
        public virtual User? User { get; set; }
    }
}
