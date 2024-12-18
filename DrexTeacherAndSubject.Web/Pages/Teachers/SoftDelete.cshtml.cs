using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class SoftDeleteModel : PageModel
    {
        [BindProperty]
        public Teacher Item { get; set; }

        private readonly ITeacherService _teacherService;

        public SoftDeleteModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task OnGet(Guid? id = null)
        {
            if (id == null)
            {
                Item = null;
                return;
            }

            Item = await _teacherService.GetByIdAsync(id.Value);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Item == null)
            {
                return NotFound();
            }

            await _teacherService.SoftDeleteAsync(Item.Id); // Mark as deleted
            return RedirectToPage("/Teachers/Index"); // Redirect to the teacher list after soft deletion
        }
    }
}
