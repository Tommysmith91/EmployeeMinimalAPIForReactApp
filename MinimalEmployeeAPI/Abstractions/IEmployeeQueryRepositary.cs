using EmployeeAPI.Entities;
using EmployeeAPI.ResponseModels;

namespace EmployeeAPI.Abstractions
{
    public interface IEmployeeQueryRepositary
    {
        public Task<IResponseDataModel<IEnumerable<Employee>>> GetEmployees();
        public Task<IResponseDataModel<Employee>> GetEmployee(int employeeId);
    }
}
