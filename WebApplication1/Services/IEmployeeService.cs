using WebApplication1.DTO;
using WebApplication1.DTO.Employee;

namespace WebApplication1.Services
{
    public interface IEmployeeService
    {
        public Task<(List<EmployeeDto> Items, int Total)> GetFilteredAsync(
    EmployeeFilter filter, int page, int pageSize);
        public Task<EmployeeEditDto?> GetForEditAsync(Guid id);
        public Task<Guid> CreateAsync(CreateEmployeeDto dto);
        public Task<bool> UpdateAsync(UpdateEmployeeDto dto);
        public Task<bool> DeleteAsync(Guid id);
        public Task<List<DepartmentDto>> GetDepartmentsAsync();
        public Task<List<EducationDto>> GetEducationsAsync();
        Task<long> GetMaxStuffNumberAsync();
    }
}