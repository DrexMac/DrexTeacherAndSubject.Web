using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string? Title { get; set; }
        public int CreditHours { get; set; }

        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}
