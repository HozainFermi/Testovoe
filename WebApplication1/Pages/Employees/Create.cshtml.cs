using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTO;
using WebApplication1.Services;
using WebApplication1.DTO.Employee;

namespace WebApplication1.Pages.Employees
    {
        public class CreateModel : PageModel
        {
            private readonly IEmployeeService _employeeService;

            public CreateModel(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

            [BindProperty]
            public CreateEmployeeDto Input { get; set; } = new();
            public long SuggestedStuffNumber { get; set; }
            public List<DepartmentDto> Departments { get; set; } = new();
            public List<EducationDto> Educations { get; set; } = new();

            public async Task OnGetAsync()
            {
                long maxNumber = await _employeeService.GetMaxStuffNumberAsync();
                SuggestedStuffNumber = maxNumber + 1;
            
                Input.StuffNumber = SuggestedStuffNumber;

            Departments = await _employeeService.GetDepartmentsAsync();
                Educations = await _employeeService.GetEducationsAsync();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    Departments = await _employeeService.GetDepartmentsAsync();
                    Educations = await _employeeService.GetEducationsAsync();
                    return Page();
                }

                await _employeeService.CreateAsync(Input);
                return RedirectToPage("./Index");
            }
        }
    }

