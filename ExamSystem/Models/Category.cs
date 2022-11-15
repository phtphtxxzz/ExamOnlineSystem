using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class Category
    {
        public Category()
        {
            Exams = new HashSet<Exam>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
