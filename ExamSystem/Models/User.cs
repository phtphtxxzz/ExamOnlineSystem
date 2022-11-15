using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class User
    {
        public User()
        {
            CompletedExams = new HashSet<CompletedExam>();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }

        public virtual Account UserNavigation { get; set; } = null!;
        public virtual ICollection<CompletedExam> CompletedExams { get; set; }
    }
}
