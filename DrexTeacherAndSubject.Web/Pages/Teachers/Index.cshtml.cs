using DrexTeacherAndSubject.Contracts;
using DrexTeacherAndSubject.EntityFramework;
using DrexTeacherAndSubject.Models;
using DrexTeacherAndSubject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrexTeacherAndSubject.Web.Pages.Teachers
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;

        public List<Teacher> Teachers { get; set; }

        [BindProperty]
        public SearchParameters? SearchParams { get; set; }

        public Index(ILogger<Index> logger, ITeacherService teacherService, ISubjectService subjectService)
        {
            _logger = logger;
            _teacherService = teacherService;
            _subjectService = subjectService;
        }

        public async Task OnGet(string? keyword = "", string? searchBy = "", string? sortBy = null, string? sortAsc = "true", int pageSize = 5, int pageIndex = 1)
        {
            if (SearchParams == null)
            {
                SearchParams = new SearchParameters()
                {
                    SortBy = sortBy,
                    SortAsc = sortAsc == "true",
                    SearchBy = searchBy,
                    Keyword = keyword,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }

            var teachers = (await _teacherService.GetAllAsync()).ToList();

            if (!string.IsNullOrEmpty(SearchParams.SearchBy) && !string.IsNullOrEmpty(SearchParams.Keyword))
            {
                if (SearchParams.SearchBy.ToLower() == "teacher")
                {
                    teachers = teachers.Where(t => t.Name?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
                }
                else if (SearchParams.SearchBy.ToLower() == "department")
                {
                    teachers = teachers.Where(t => t.Department?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
                }
                else if (SearchParams.SearchBy.ToLower() == "subject")
                {
                    teachers = teachers.Where(t => t.Subjects?.Any(s => s.Title?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true) == true).ToList();
                }
            }
            else if (string.IsNullOrEmpty(SearchParams.SearchBy) && !string.IsNullOrEmpty(SearchParams.Keyword))
            {
                teachers = teachers.Where(t => t.Name?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
            }

            if (SearchParams.SortBy == null || SearchParams.SortAsc == null)
            {
                Teachers = teachers;
                goto page;
            }

            if (SearchParams.SortBy.ToLower() == "teacher" && SearchParams.SortAsc == true)
            {
                Teachers = teachers.OrderBy(t => t.Name).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "teacher" && SearchParams.SortAsc == false)
            {
                Teachers = teachers.OrderByDescending(t => t.Name).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "department" && SearchParams.SortAsc == true)
            {
                Teachers = teachers.OrderBy(t => t.Department).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "department" && SearchParams.SortAsc == false)
            {
                Teachers = teachers.OrderByDescending(t => t.Department).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "subject" && SearchParams.SortAsc == true)
            {
                Teachers = teachers.OrderBy(t => t.Subjects?.FirstOrDefault()?.Title).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "subject" && SearchParams.SortAsc == false)
            {
                Teachers = teachers.OrderByDescending(t => t.Subjects?.FirstOrDefault()?.Title).ToList();
            }
            else
            {
                Teachers = teachers;
            }

        page:
            // Paging
            int totalPages = (int)Math.Ceiling((double)Teachers.Count / SearchParams.PageSize.Value);
            int skip = (SearchParams.PageIndex.Value - 1) * SearchParams.PageSize.Value;
            Teachers = Teachers.Skip(skip).Take(SearchParams.PageSize.Value).ToList();
            SearchParams.PageCount = totalPages;
        }

    }

    public class SearchParameters
    {
        public string? SearchBy { get; set; }
        public string? Keyword { get; set; }
        public string? SortBy { get; set; }
        public bool? SortAsc { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public int? PageCount { get; set; }
    }
}
