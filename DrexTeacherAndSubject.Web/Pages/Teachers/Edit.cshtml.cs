using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class EditModel : PageModel
    {
        private readonly ITeacherService _teacherService;

        [BindProperty]
        public Teacher Teacher { get; set; }

        public EditModel(ITeacherService teacherService)
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

            // Add new subject logic if checkbox is checked
            if (Request.Form["addSubject"] == "on")
            {
                var newSubjectTitle = Request.Form["NewSubjectTitle"];
                var newSubjectCredits = Request.Form["NewSubjectCreditHours"];

                if (!string.IsNullOrEmpty(newSubjectTitle) && !string.IsNullOrEmpty(newSubjectCredits))
                {
                    var newSubject = new Subject
                    {
                        Title = newSubjectTitle,
                        CreditHours = int.Parse(newSubjectCredits)
                    };

                    // Add the new subject to the teacher's subjects list
                    Teacher.Subjects.Add(newSubject);
                }
            }

            // Update teacher in the database
            await _teacherService.UpdateAsync(Teacher);

            return RedirectToPage("/Teachers/Index");

        }
    }
}
