using EmployeeAPI.Abstractions;
using EmployeeAPI.Entities;
using EmployeeAPI.Models;
using EmployeeAPI.ResponseModels;

namespace EmployeeAPI.Concrete
{
    public class EmployeeCommandRepositary : IEmployeeCommandRepositary
    {
        private readonly EmployeeDb _employeeDb;

        public EmployeeCommandRepositary(EmployeeDb employeeDb)
        {
            _employeeDb = employeeDb;
        }

        public async Task<IResponseDataModel<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                await _employeeDb.AddAsync(employee);
                return await _employeeDb.SaveChangesAsync() == 1 ?
                new ResponseDataModel<Employee>
                {
                    Data = employee,
                    Success = true

                } : new ResponseDataModel<Employee>
                {
                    Success = false
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<IResponseModel> DeleteEmployee(int employeeId)
        {
            var id = employeeId;
            var employee = await _employeeDb.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResponseModel
                {
                    Success = false
                };
            }
            _employeeDb.Employees.Remove(employee);
            await _employeeDb.SaveChangesAsync();
            return new ResponseModel()
            {
                Success = true
            };
        }

        public async Task<IResponseModel> UpdateEmployee(Employee employee, int id)
        {
            var existingEmployee = await _employeeDb.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = $"Employee with Id {id} does not exist"
                };
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Age = employee.Age;
            existingEmployee.HasRightToWork = employee.HasRightToWork;
            existingEmployee.AddressLine1 = employee.AddressLine1;
            existingEmployee.AddressLine2 = employee.AddressLine2;
            existingEmployee.Postcode = employee.Postcode;
            existingEmployee.CityTown = employee.CityTown;
            existingEmployee.Country = employee.Country;
            existingEmployee.StartOfEmployment = employee.StartOfEmployment;


            await _employeeDb.SaveChangesAsync();
            return new ResponseModel
            {
                Success = true
            };
        }
    }
}
