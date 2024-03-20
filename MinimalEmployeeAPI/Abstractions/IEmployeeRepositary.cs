using EmployeeAPI.Models;

namespace EmployeeAPI.Abstractions
{
    public interface IEmployeeRepositary
    {
        public Task<IResponseDataModel<Employee>> CreateEmployee(Employee employee);
        public Task<IResponseDataModel<IEnumerable<Employee>>> GetEmployees();
        public Task<IResponseModel> DeleteEmployee(int employeeId);
        public Task<IResponseDataModel<Employee>> GetEmployee(int employeeId);

        public Task<IResponseModel> UpdateEmployee(Employee employee,int id);

    }
}
