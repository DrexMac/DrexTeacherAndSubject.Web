using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
