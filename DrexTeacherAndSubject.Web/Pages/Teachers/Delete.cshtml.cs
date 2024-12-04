using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class Delete : PageModel
    {
        [BindProperty]
        public Teacher? Item { get; set; }
        private readonly ITeacherService _teacherService;
        private readonly ILogger<Delete> _logger;

        public Delete(ILogger<Delete> logger, ITeacherService teacherService)
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
            else
            {
                Item = await _teacherService.GetByIdAsync(id.Value);
                return;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (Item == null)
            {
                Item = null;
                return Page();
            }
            else
            {
                await _teacherService.DeleteAsync(Item.Id);
                return RedirectPermanent("~/teachers");  
            }
        }
    }
}
