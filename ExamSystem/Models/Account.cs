using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class Account
    {
        public Account()
        {
            Exams = new HashSet<Exam>();
        }

        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string Email { get; set; } = null!;

        public virtual User? User { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
