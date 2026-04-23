namespace WebApplication1.DTO.Employee
{
    public class EmployeeNoPromotionDto
    {
        public Guid Id { get; set; }
        public long StuffNumber { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
    }
}
