using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class Category
    {
        public Category()
        {
            Questions = new HashSet<Question>();
            Records = new HashSet<Record>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int CategoryValue { get; set; }

        [JsonIgnore]
        public virtual ICollection<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Record> Records { get; set; }
    }
}
