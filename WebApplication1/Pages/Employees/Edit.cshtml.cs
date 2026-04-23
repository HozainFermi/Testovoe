using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.Services;

namespace WebApplication1.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public EditModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty]
        public UpdateEmployeeDto Input { get; set; } = new();

        public List<DepartmentDto> Departments { get; set; } = new();
        public List<EducationDto> Educations { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var employee = await _employeeService.GetForEditAsync(id);
            if (employee == null) return NotFound();

            Input = new UpdateEmployeeDto
            {
                Id = employee.Id,
                StuffNumber = employee.StuffNumber,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                FiredDate = employee.FiredDate,
                DepartmentId = employee.DepartmentId,
                EducationId = employee.EducationId
            };

            await LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return Page();
            }

            var success = await _employeeService.UpdateAsync(Input);
            if (!success) return NotFound();

            return RedirectToPage("./Index");
        }

        private async Task LoadDropdowns()
        {
            Departments = await _employeeService.GetDepartmentsAsync();
            Educations = await _employeeService.GetEducationsAsync();
        }
    }
}