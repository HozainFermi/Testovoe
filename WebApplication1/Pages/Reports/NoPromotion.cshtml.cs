using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.Services;

namespace WebApplication1.Pages.Reports
{
    public class NoPromotionModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public NoPromotionModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public List<EmployeeNoPromotionDto> Employees { get; set; } = new();

        public async Task OnGetAsync()
        {
            Employees = await _promotionService.GetEmployeesWithoutPromotionAsync();
        }

        public async Task<IActionResult> OnPostExportExcelAsync()
        {
            var excelData = await _promotionService.ExportEmployeesWithoutPromotionAsync();
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NoPromotions.xlsx");
        }


    }
}