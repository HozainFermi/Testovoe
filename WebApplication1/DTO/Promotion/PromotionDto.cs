namespace WebApplication1.DTO.Promotion
{
    public class PromotionDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeFullName { get; set; } = string.Empty;
        public long StuffNumber { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public float IncreasingPercent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
