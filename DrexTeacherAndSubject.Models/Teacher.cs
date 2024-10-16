using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }

        public ICollection<TeacherSubject>? TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}
