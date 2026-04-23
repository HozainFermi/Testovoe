namespace WebApplication1.Data.Entities
{
    public class Promotion: BaseEntity
    {
        public float IncreasingPercent { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}