using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class CreateModel : PageModel
    {
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;

        [BindProperty]
        public CreateTeacherViewModel Input { get; set; }

        public CreateModel(ITeacherService teacherService, ISubjectService subjectService)
        {
            _teacherService = teacherService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> OnGet()
        {
            var subjects = await _subjectService.GetAllAsync();
            ViewData["Subjects"] = subjects;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var subjects = await _subjectService.GetAllAsync();
                ViewData["Subjects"] = subjects;
                return Page();
            }

            var teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                Name = Input.Name,
                Department = Input.Department,
                Subjects = Input.SubjectIds?.Select(subjectId => new Subject { Id = subjectId }).ToList()
            };

            await _teacherService.AddAsync(teacher);
            return RedirectToPage("/Teachers/Index");



        }
    }

    public class CreateTeacherViewModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public IEnumerable<Guid>? SubjectIds { get; set; }
    }
}
