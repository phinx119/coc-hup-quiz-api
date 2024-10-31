using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class Record
    {
        public int RecordId { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyId { get; set; }
        public int? HighScore { get; set; }
        public DateTime? RecordDate { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual Difficulty? Difficulty { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
