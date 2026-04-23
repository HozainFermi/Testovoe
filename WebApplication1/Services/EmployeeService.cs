using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.DTO;
using WebApplication1.DTO.Employee;
using WebApplication1.Mappers;

namespace WebApplication1.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<EmployeeDto> Items, int Total)> GetFilteredAsync(
            EmployeeFilter filter, int page, int pageSize)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Education)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(e => e.FullName.Contains(filter.FullName));

            if (filter.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filter.DepartmentId);

            if (filter.EducationId.HasValue)
                query = query.Where(e => e.EducationId == filter.EducationId);

            var total = await query.CountAsync();
            var employees = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = employees.Select(e => EmployeeMapper.ToDto(e)).ToList();

            return (items, total);
        }

        public async Task<EmployeeEditDto?> GetForEditAsync(Guid id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return EmployeeMapper.ToEditDto(employee);
        }

        public async Task<Guid> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = EmployeeMapper.ToEntity(dto);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<bool> UpdateAsync(UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees.FindAsync(dto.Id);
            if (employee == null) return false;

            employee.StuffNumber = dto.StuffNumber;
            employee.FullName = dto.FullName;
            employee.BirthDate = dto.BirthDate;
            employee.HireDate = dto.HireDate;
            employee.FiredDate = dto.FiredDate;
            employee.DepartmentId = dto.DepartmentId;
            employee.EducationId = dto.EducationId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DepartmentDto>> GetDepartmentsAsync()
        {
            var departments = await _context.Departments.ToListAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.DepartmentName
            }).ToList();
        }

        public async Task<List<EducationDto>> GetEducationsAsync()
        {
            var educations = await _context.Educations.ToListAsync();

            return educations.Select(e => new EducationDto
            {
                Id = e.Id,
                Name = e.EducationLevel.ToString()  
            }).ToList();
        }

        public async Task<long> GetMaxStuffNumberAsync()
        {
            return await _context.Employees.MaxAsync(e=>e.StuffNumber);
        }
    }
}
