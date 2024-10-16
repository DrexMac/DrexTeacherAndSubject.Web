using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly ITeacherService _teacherService; 
        private readonly ISubjectService _subjectService;

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public Index(ILogger<Index> logger, ITeacherService teacherService, ISubjectService subjectService)
        {
            _logger = logger;
            _teacherService = teacherService; 
            _subjectService = subjectService; 
        }

        public async Task OnGetAsync() 
        {
            Teachers = (await _teacherService.GetAllAsync()).ToList(); 
            Subjects = (await _subjectService.GetAllAsync()).ToList(); 
        }
    }
}
