using WebApplication1.Data.Entities;
using WebApplication1.DTO.Employee;

namespace WebApplication1.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                StuffNumber = employee.StuffNumber,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                FiredDate = employee.FiredDate,
                DepartmentName = employee.Department?.DepartmentName ?? "",
                EducationName = employee.Education?.EducationLevel.ToString() ?? ""
            };
        }

        public static EmployeeEditDto ToEditDto(Employee employee)
        {
            return new EmployeeEditDto
            {
                Id = employee.Id,
                StuffNumber = employee.StuffNumber,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                FiredDate = employee.FiredDate,
                DepartmentId = employee.DepartmentId,
                EducationId = employee.EducationId
            };
        }

        public static Employee ToEntity(CreateEmployeeDto dto)
        {
            return new Employee
            {
                Id = Guid.NewGuid(),
                StuffNumber = dto.StuffNumber,
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                HireDate = dto.HireDate,
                FiredDate = dto.FiredDate,
                DepartmentId = dto.DepartmentId,
                EducationId = dto.EducationId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
