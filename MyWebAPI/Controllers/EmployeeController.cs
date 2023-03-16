using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet("/Employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepository.GetAllEmployeesAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        [HttpGet("/Employees/{id:min(1)}")]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            var employee = await employeeRepository.GetEmployeesByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost("/Employees/Add")]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeesModel employeesModel)
        {
          var id = await employeeRepository.AddEmployeeAsync(employeesModel);
            return CreatedAtAction(nameof(AddEmployee),id);

        }
        [HttpPut("/Employees/Update/{id:min(1)}")]
        public async Task<IActionResult> UpdateEmployee(int id,[FromBody]EmployeesModel employeesModel)
        {
            Console.WriteLine(employeesModel);
            await employeeRepository.UpdateEmployeeAsync(employeesModel, id);
            return Ok("Successful");
        }
        [HttpPatch("/Employees/Patch/{id:min(1)}")]
        public async Task<IActionResult> UpdateEmployeePatch(int id, [FromBody] JsonPatchDocument JsonModel)
        {
            Console.WriteLine(JsonModel);
            await employeeRepository.UpdateEmployeePatchAsync(JsonModel, id);
            return Ok("Successful");
        }

        [HttpDelete("/Employees/Delete/{id:min(1)}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await employeeRepository.DeleteEmployeeAsync(id);
            return Ok("Deleted");
        }


    }
}
