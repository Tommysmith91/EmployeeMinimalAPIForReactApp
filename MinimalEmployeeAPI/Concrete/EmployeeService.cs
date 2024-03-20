using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;
using FluentValidation;
using System.Runtime.CompilerServices;

namespace EmployeeAPI.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepositary _employeeRepositary;
        private readonly IValidator<Employee> _employeeValidator;

        public EmployeeService(IEmployeeRepositary employeeRepositary,IValidator<Employee> employeeValidator)
        {
            _employeeRepositary = employeeRepositary;
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
                var result = await _employeeRepositary.CreateEmployee(employee);
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
                var result = await _employeeRepositary.DeleteEmployee(employeeId);
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
                var result = await _employeeRepositary.GetEmployee(employeeId);
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
            var result = await _employeeRepositary.GetEmployees();            
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
                var result = await _employeeRepositary.UpdateEmployee(employee, id);
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
