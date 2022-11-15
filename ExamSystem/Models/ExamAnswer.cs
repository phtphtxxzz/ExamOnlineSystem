using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class ExamAnswer
    {
        public int ExamAnswerId { get; set; }
        public int? CompletedExamId { get; set; }
        public int? QuestionId { get; set; }
        public int? Answer { get; set; }
        public string? AnswerContent { get; set; }
        public int? CorrectAnswer { get; set; }

        public virtual CompletedExam? CompletedExam { get; set; }
        public virtual Question? Question { get; set; }
    }
}
