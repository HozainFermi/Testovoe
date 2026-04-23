namespace WebApplication1.Data.Entities
{
    public class Education: BaseEntity
    {    
        public EducationLevel EducationLevel { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}