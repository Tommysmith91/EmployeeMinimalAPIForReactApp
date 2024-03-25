using EmployeeAPI.Models;

namespace EmployeeAPI.Abstractions
{
    public interface IEmployeeCommandRepositary
    {
        public Task<IResponseDataModel<Employee>> CreateEmployee(Employee employee);
        public Task<IResponseModel> DeleteEmployee(int employeeId);
        public Task<IResponseModel> UpdateEmployee(Employee employee, int id);
    }
}
