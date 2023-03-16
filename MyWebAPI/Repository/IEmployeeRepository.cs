using Microsoft.AspNetCore.JsonPatch;
using MyWebAPI.Models;

namespace MyWebAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(EmployeesModel employee);
        Task DeleteEmployeeAsync(int id);
        Task<List<EmployeesModel>> GetAllEmployeesAsync();
        Task<EmployeesModel> GetEmployeesByIdAsync(int id);
        Task UpdateEmployeeAsync(EmployeesModel employee, int id);
        Task UpdateEmployeePatchAsync(JsonPatchDocument employeesModel, int id);
    }
}
