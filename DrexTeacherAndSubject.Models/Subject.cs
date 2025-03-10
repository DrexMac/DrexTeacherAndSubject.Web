﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int CreditHours { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
