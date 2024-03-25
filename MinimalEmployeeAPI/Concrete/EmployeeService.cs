using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;
using FluentValidation;
using System.Runtime.CompilerServices;

namespace EmployeeAPI.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeCommandRepositary _employeeCommandRepositary;
        private readonly IEmployeeQueryRepositary _employeeQueryRepositary;
        private readonly IValidator<Employee> _employeeValidator;

        public EmployeeService(IEmployeeCommandRepositary employeeCommandRepositary,IEmployeeQueryRepositary employeeQueryRepositary,IValidator<Employee> employeeValidator)
        {
            _employeeCommandRepositary = employeeCommandRepositary;
            _employeeQueryRepositary = employeeQueryRepositary;
            _employeeValidator = employeeValidator;
        }
        public async Task<IResponseDataModel<EmployeeDTO>> CreateEmployee(Employee employee)
        {
            try
            {
                var validationResult = _employeeValidator.Validate(employee);
                if(validationResult.IsValid == false)
                {
                    return new ResponseDataModel<EmployeeDTO>()
                    {
                        Success = false,
                        Message = string.Join(" ", validationResult.Errors.Select(x=> x.ErrorMessage))
                    };
                }
                var result = await _employeeCommandRepositary.CreateEmployee(employee);
                if (result.Success)
                {
                    return new ResponseDataModel<EmployeeDTO>
                    {
                        Success = true,
                        Data = new EmployeeDTO(result.Data)
                    };
                }
                return new ResponseDataModel<EmployeeDTO>
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
            try
            {
                var result = await _employeeCommandRepositary.DeleteEmployee(employeeId);
                if (result.Success)
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
                return new ResponseModel
                {
                    Success = false
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<IResponseDataModel<EmployeeDTO>> GetEmployee(int employeeId)
        {
            try
            {
                var result = await _employeeQueryRepositary.GetEmployee(employeeId);
                if (result.Success)
                {
                    return new ResponseDataModel<EmployeeDTO>
                    {
                        Data = new EmployeeDTO(result.Data),
                        Success = true
                    };
                }
                return new ResponseDataModel<EmployeeDTO>()
                {
                    Success = false
                };
            }
            catch
            {
                throw;
            }

        }

        public async Task<IResponseDataModel<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var result = await _employeeQueryRepositary.GetEmployees();            
            return new ResponseDataModel<IEnumerable<EmployeeDTO>>
            {
                Data = result.Data.Select(x => new EmployeeDTO(x)),
                Success = true
            };
        }

        public async Task<IResponseModel> UpdateEmployee(Employee employee, int id)
        {
            try
            {
                var result = await _employeeCommandRepositary.UpdateEmployee(employee, id);
                if (result.Success)
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
                return new ResponseModel
                {
                    Success = false
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
