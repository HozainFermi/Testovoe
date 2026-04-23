using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; 
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.DTO.Promotion;
using WebApplication1.Mappers;

namespace WebApplication1.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly AppDbContext _context;

        public PromotionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PromotionDto>> GetPromotionListAsync()
        {
            var promotions = await _context.Promotions
                .Include(p => p.Employee)
                .ThenInclude(e => e.Department)
                .ToListAsync();

            return promotions.Select(p => PromotionMapper.ToDto(p)).ToList();
        }

        public async Task AddToPromotionListAsync(List<CreatePromotionDto> promotions)
        {
            var entities = promotions.Select(p => new Promotion
            {
                Id = Guid.NewGuid(),
                EmployeeId = p.EmployeeId,
                IncreasingPercent = p.IncreasingPercent,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _context.Promotions.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> ExportPromotionsToExcelAsync()
        {
            var promotions = await GetPromotionListAsync();

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Предстоящее повышение ЗП");

            // Заголовки
            sheet.Cells[1, 1].Value = "ФИО";
            sheet.Cells[1, 2].Value = "Табельный номер";
            sheet.Cells[1, 3].Value = "Подразделение";
            sheet.Cells[1, 4].Value = "Процент повышения";
            sheet.Cells[1, 5].Value = "Дата добавления";

            // Данные
            for (int i = 0; i < promotions.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = promotions[i].EmployeeFullName;
                sheet.Cells[i + 2, 2].Value = promotions[i].StuffNumber;
                sheet.Cells[i + 2, 3].Value = promotions[i].DepartmentName;
                sheet.Cells[i + 2, 4].Value = promotions[i].IncreasingPercent;
                sheet.Cells[i + 2, 5].Value = promotions[i].CreatedAt.ToString("dd.MM.yyyy");
            }

            sheet.Cells.AutoFitColumns();
            return await package.GetAsByteArrayAsync();
        }
        public async Task<List<EmployeeNoPromotionDto>> GetEmployeesWithoutPromotionAsync()
        {
            var sql = @"
                SELECT 
                    e.Id,
                    e.StuffNumber,
                    e.FullName,
                    d.DepartmentName,
                    e.HireDate
                FROM Employees e
                INNER JOIN Departments d ON e.DepartmentId = d.Id
                LEFT JOIN Promotions p ON e.Id = p.EmployeeId
                WHERE p.Id IS NULL
                  AND e.FiredDate IS NULL
                ORDER BY e.FullName";

            var result = await _context.Database
                .SqlQueryRaw<EmployeeNoPromotionDto>(sql)
                .ToListAsync();

            return result;
        }

        public async Task<byte[]> ExportEmployeesWithoutPromotionAsync()
        {            
            var sql = @"
                SELECT 
                    e.Id,
                    e.StuffNumber,
                    e.FullName,
                    d.DepartmentName,
                    e.HireDate
                FROM Employees e
                INNER JOIN Departments d ON e.DepartmentId = d.Id
                LEFT JOIN Promotions p ON e.Id = p.EmployeeId
                WHERE p.Id IS NULL
                  AND e.FiredDate IS NULL
                ORDER BY e.FullName";

            var result = await _context.Database
                .SqlQueryRaw<EmployeeNoPromotionDto>(sql)
                .ToListAsync();

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Сотрудники без повышение ЗП");

            // Заголовки
            sheet.Cells[1, 1].Value = "ФИО";
            sheet.Cells[1, 2].Value = "Табельный номер";
            sheet.Cells[1, 3].Value = "Подразделение";
            sheet.Cells[1, 4].Value = "Дата найма";            

            // Данные
            for (int i = 0; i < result.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = result[i].FullName;
                sheet.Cells[i + 2, 2].Value = result[i].StuffNumber;
                sheet.Cells[i + 2, 3].Value = result[i].DepartmentName;
                sheet.Cells[i + 2, 4].Value = result[i].HireDate.ToString("dd.MM.yyyy");                
            }
            sheet.Cells.AutoFitColumns();

            return await package.GetAsByteArrayAsync();
        }
    }
}