using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.DTO.Promotion;
using WebApplication1.Services;

namespace WebApplication1.Pages.Reports
{
    public class PromotionsModel : PageModel
    {
        private readonly IPromotionService _promotionService;
        private readonly IEmployeeService _employeeService;

        public PromotionsModel(IPromotionService promotionService, IEmployeeService employeeService)
        {
            _promotionService = promotionService;
            _employeeService = employeeService;
        }

        public List<PromotionDto> Promotions { get; set; } = new();
        public List<EmployeeDto> AllEmployees { get; set; } = new();

        [BindProperty]
        public Guid SelectedEmployeeId { get; set; }

        [BindProperty]
        public float IncreasePercent { get; set; }

        public async Task OnGetAsync()
        {
            Promotions = await _promotionService.GetPromotionListAsync();
            var (items, total) = await _employeeService.GetFilteredAsync(new EmployeeFilter(), 1, 1000);
           
            AllEmployees = items.Where(x => x.FiredDate == null).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (SelectedEmployeeId != Guid.Empty && IncreasePercent > 0)
            {
                var promotions = new List<CreatePromotionDto>
                {
                    new() { EmployeeId = SelectedEmployeeId, IncreasingPercent = IncreasePercent }
                };
                await _promotionService.AddToPromotionListAsync(promotions);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportExcelAsync()
        {
            var excelData = await _promotionService.ExportPromotionsToExcelAsync();
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Promotions.xlsx");
        }
    }
}