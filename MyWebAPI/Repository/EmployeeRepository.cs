using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;
using MyWebAPI.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace MyWebAPI.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeesContext context;

        public EmployeeRepository(EmployeesContext context)
        {
            this.context = context;
        }

        async Task<List<EmployeesModel>> IEmployeeRepository.GetAllEmployeesAsync()
        {
            var list = await this.context.Employees.ToListAsync();
            Console.WriteLine(list);
            var employeeList = await this.context.Employees.Select(e => new EmployeesModel()
            {
                emp_id = e.emp_id,
                hire_date = e.hire_date,
                first_name = e.first_name,
                last_name = e.last_name,
                salary = e.salary,
                department = e.department
            })
              .ToListAsync();
            return employeeList;
        }
        async Task<EmployeesModel> IEmployeeRepository.GetEmployeesByIdAsync(int id)
        {
            var list = await this.context.Employees.ToListAsync();
            Console.WriteLine(list);
            var employeeDetails = await this.context.Employees.Where(e => e.emp_id == id).Select(e => new EmployeesModel()
            {
                emp_id = e.emp_id,
                hire_date = e.hire_date,
                first_name = e.first_name,
                last_name = e.last_name,
                salary = e.salary,
                department = e.department
            })
              .FirstOrDefaultAsync();
            return employeeDetails;
        }
        async Task<int> IEmployeeRepository.AddEmployeeAsync(EmployeesModel employee)
        {
            var employ = new EmployeesModel()
            {
                first_name = employee.first_name,
                last_name = employee.last_name,
                salary = employee.salary,
                department = employee.department,
                hire_date = employee.hire_date
            };
            this.context.Employees.Add(employ);
            await this.context.SaveChangesAsync();
            return employ.emp_id;
        }

        async Task IEmployeeRepository.UpdateEmployeeAsync(EmployeesModel employee,int id)
        {
            var employ = new EmployeesModel()
            {
                emp_id=id,
                first_name = employee.first_name,
                last_name = employee.last_name,
                salary = employee.salary,
                department = employee.department,
                hire_date = employee.hire_date
            };
            this.context.Employees.Update(employ);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEmployeePatchAsync(JsonPatchDocument employeesModel, int id)
        {
            var employee = await this.context.Employees.FindAsync(id);
            if (employee != null)
            {
                employeesModel.ApplyTo(employee);
                await this.context.SaveChangesAsync();
            }
            
        }

        async Task IEmployeeRepository.DeleteEmployeeAsync(int id)
        {
            var employee = new EmployeesModel()
            {
                emp_id = id
            };
            this.context.Employees.Remove(employee);
            await this.context.SaveChangesAsync();
        }
    }
}
