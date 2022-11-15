using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class Exam
    {
        public Exam()
        {
            CompletedExams = new HashSet<CompletedExam>();
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public int? Owner { get; set; }
        public string? ExamName { get; set; }
        public int? Time { get; set; }
        public int? CategoryId { get; set; }
        public int? Numofquestion { get; set; }
        public string? Pictures { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Account? OwnerNavigation { get; set; }
        public virtual ICollection<CompletedExam> CompletedExams { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
