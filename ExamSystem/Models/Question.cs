using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class Question
    {
        public Question()
        {
            ExamAnswers = new HashSet<ExamAnswer>();
        }

        public int QuestionId { get; set; }
        public string? Question1 { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? ExamId { get; set; }

        public virtual Exam? Exam { get; set; }
        public virtual ICollection<ExamAnswer> ExamAnswers { get; set; }
    }
}
