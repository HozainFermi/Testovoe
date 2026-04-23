namespace WebApplication1.DTO.Employee
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        public long StuffNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? FiredDate { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid EducationId { get; set; }
    }
}
