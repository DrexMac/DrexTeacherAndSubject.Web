using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Teacher? Item { get; set; }

        private readonly ITeacherService _teacherService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ILogger<DeleteModel> logger, ITeacherService teacherService)
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

            
            await _teacherService.DeleteAsync(Item.Id);
            return RedirectToPage("/Teachers/Index"); 
        }
    }
}
