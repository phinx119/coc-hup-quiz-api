using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class Difficulty
    {
        public Difficulty()
        {
            Questions = new HashSet<Question>();
            Records = new HashSet<Record>();
        }

        public int DifficultyId { get; set; }
        public string DifficultyName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record> Records { get; set; }
    }
}
