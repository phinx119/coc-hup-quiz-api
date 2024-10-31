using System;
using System.Collections.Generic;

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

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
