using WebApplication1.DTO.Employee;
using WebApplication1.DTO.Promotion;

namespace WebApplication1.Services
{
    public interface IPromotionService
    {
        /// <summary>
        /// Получить список сотрудников на повышение
        /// </summary>
        Task<List<PromotionDto>> GetPromotionListAsync();

        /// <summary>
        /// Добавить сотрудников в список на повышение
        /// </summary>
        Task AddToPromotionListAsync(List<CreatePromotionDto> promotions);

        /// <summary>
        /// Выгрузить список на повышение в Excel
        /// </summary>
        Task<byte[]> ExportPromotionsToExcelAsync();

        /// <summary>
        /// Получить список сотрудников без повышения 
        /// </summary>
        Task<List<EmployeeNoPromotionDto>> GetEmployeesWithoutPromotionAsync();
        
        /// <summary>
        /// Экспортировать работников без повышения зп в excel
        /// </summary>
        /// <returns></returns>
        public Task<byte[]> ExportEmployeesWithoutPromotionAsync();
    }
}
