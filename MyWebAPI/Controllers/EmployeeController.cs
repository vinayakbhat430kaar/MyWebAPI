using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.Repository;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //for bind property to work we need to send data as formdata orr send data from Form-data form from postman
    //for custom model binding we can referr below videos
    //https://www.youtube.com/watch?v=3-RJ0p0hlLs&list=PLaFzfwmPR7_IPzBR4AI0eoojmIdTFJmHs&index=73
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
        //we can add all the validations like min max alpha numbers etc
        [HttpPost("/Employees/Add")]

        //for these validations to work we should assign the same name!,
        //[FromQuery] takes data from the query URL's, query starts after ?
        //if we send data through the body, we can implement the complex types(Class objects)
        //if we send data through the url/ as a query parameter it will be considered as primitive types only


        //[FromRoute] will force the variable to take data only from the route. data sent from body will be simply ignored.
        //[FromBody] will force the variable to data from the body only.
        //[FromForm] will accept the data sent from the help of FormData object type or form-data type from postman.

        //[FromHeader] will accept and assign the data sent from the headers
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
