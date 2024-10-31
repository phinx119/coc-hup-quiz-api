using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class User
    {
        public User()
        {
            Questions = new HashSet<Question>();
            Records = new HashSet<Record>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public int? BirthYear { get; set; }

        [JsonIgnore]
        public virtual ICollection<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record> Records { get; set; }
    }
}
