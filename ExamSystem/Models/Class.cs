using System;
using System.Collections.Generic;

namespace ExamSystem.Models
{
    public partial class Class
    {
        public Class()
        {
            Exams = new HashSet<Exam>();
        }

        public int ClassId { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
