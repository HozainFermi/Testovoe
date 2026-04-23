using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.Services;

namespace WebApplication1.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public IndexModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty(SupportsGet = true)]
        public EmployeeFilter Filter { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; } = 10;
        public int Total { get; set; }
        public List<EmployeeDto> Employees { get; set; } = new();
        public List<DepartmentDto> Departments { get; set; } = new();
        public List<EducationDto> Educations { get; set; } = new();

        public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);

        public async Task OnGetAsync()
        {
            var (items, total) = await _employeeService.GetFilteredAsync(Filter, CurrentPage, PageSize);
            Employees = items;
            Total = total;

            Departments = await _employeeService.GetDepartmentsAsync();
            Educations = await _employeeService.GetEducationsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
