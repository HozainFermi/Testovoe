using WebApplication1.Data.Entities;
using WebApplication1.DTO.Promotion;

namespace WebApplication1.Mappers
{
    public static class PromotionMapper
    {
        public static PromotionDto ToDto(Promotion p)
        {
            return new PromotionDto
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                EmployeeFullName = p.Employee.FullName,
                StuffNumber = p.Employee.StuffNumber,
                DepartmentName = p.Employee.Department?.DepartmentName ?? "",
                IncreasingPercent = p.IncreasingPercent,
                CreatedAt = p.CreatedAt
            };
        }
    }
}
