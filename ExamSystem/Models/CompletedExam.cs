using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class CompletedExam
    {
        public CompletedExam()
        {
            ExamAnswers = new HashSet<ExamAnswer>();
        }

        public int CompletedExamId { get; set; }
        public int? ExamId { get; set; }
        public int? UserId { get; set; }
        public double? Mark { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Exam? Exam { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ExamAnswer> ExamAnswers { get; set; }
    }
}
