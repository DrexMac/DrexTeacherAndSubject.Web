using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class UpdateModel : PageModel
    {
        private readonly ITeacherService _teacherService;

        [BindProperty]
        public Teacher Teacher { get; set; }

        public UpdateModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher = await _teacherService.GetByIdAsync(id.Value);

            if (Teacher == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Request.Form["addSubject"] == "on")
            {
                var newSubjectTitle = Request.Form["NewSubjectTitle"];
                var newSubjectCredits = Request.Form["NewSubjectCrUpdateHours"];

                if (!string.IsNullOrEmpty(newSubjectTitle) && !string.IsNullOrEmpty(newSubjectCredits))
                {
                    var newSubject = new Subject
                    {
                        Title = newSubjectTitle,
                        CreditHours = int.Parse(newSubjectCredits)
                    };

                    Teacher.Subjects.Add(newSubject);
                }
            }

            await _teacherService.UpdateAsync(Teacher);

            return RedirectToPage("/Teachers/Index");

        }
    }
}
