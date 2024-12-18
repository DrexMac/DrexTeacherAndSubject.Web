using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class UndeleteModel : PageModel
    {
        [BindProperty]
        public Teacher? Item { get; set; }

        private readonly ITeacherService _teacherService;
        private readonly ILogger<UndeleteModel> _logger;

        public UndeleteModel(ILogger<UndeleteModel> logger, ITeacherService teacherService)
        {
            _logger = logger;
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

            await _teacherService.UndeleteAsync(Item.Id);
            return RedirectToPage("/Teachers/Index");
        }
    }
}
