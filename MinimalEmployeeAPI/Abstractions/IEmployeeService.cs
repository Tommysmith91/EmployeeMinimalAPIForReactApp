using EmployeeAPI.Models;

namespace EmployeeAPI.Abstractions
{
    public interface IEmployeeService
    {
        public Task<IResponseDataModel<EmployeeDTO>> CreateEmployee(Employee employee);
        public Task<IResponseDataModel<IEnumerable<EmployeeDTO>>> GetEmployees();
        public Task<IResponseModel> DeleteEmployee(int employeeId);
        public Task<IResponseDataModel<EmployeeDTO>> GetEmployee(int employeeId);

        public Task<IResponseModel> UpdateEmployee(Employee employee, int id);
    }
}
