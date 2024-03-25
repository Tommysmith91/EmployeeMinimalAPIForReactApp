using EmployeeAPI.Abstractions;
using EmployeeAPI.Entities;
using EmployeeAPI.Models;
using EmployeeAPI.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Concrete
{
    public class EmployeeQueryRepositary : IEmployeeQueryRepositary
    {
        private readonly EmployeeDb _employeeDb;

        public EmployeeQueryRepositary(EmployeeDb employeeDb)
        {
            _employeeDb = employeeDb;
        } 

        public async Task<IResponseDataModel<Employee>> GetEmployee(int employeeId)
        {
            var id = employeeId;
            var employee = await _employeeDb.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResponseDataModel<Employee>
                {
                    Success = false,
                    Message = "Employee Not Found"
                };
            }
            return new ResponseDataModel<Employee>
            {
                Success = true,
                Data = employee
            };
        }

        public async Task<IResponseDataModel<IEnumerable<Employee>>> GetEmployees()
        {
            return new ResponseDataModel<IEnumerable<Employee>>
            {
                Data = await _employeeDb.Employees.ToListAsync(),
                Success = true
            };
        }
    }
}
