namespace WebApplication1.Data.Entities
{
    public class Employee: BaseEntity
    {     
        public long StuffNumber { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid EducationId { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? FiredDate { get; set; }
        public Department Department { get; set; }
        public Education Education { get; set; }
        public Promotion? Promotion { get; set; }

    }
}