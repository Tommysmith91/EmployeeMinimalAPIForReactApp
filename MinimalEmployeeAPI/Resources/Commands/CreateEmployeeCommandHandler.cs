using EmployeeAPI.Abstractions;
using EmployeeAPI.Concrete;
using EmployeeAPI.Entities;
using EmployeeAPI.Models;
using EmployeeAPI.ResponseModels;
using FluentValidation;
using MediatR;
using System.Security.AccessControl;

namespace EmployeeAPI.Resources.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand,IResponseDataModel<EmployeeDTO>>
    {
        private readonly IEmployeeCommandRepositary _commandRepositary;
        private readonly IValidator<Employee> _employeeValidator;
        public CreateEmployeeCommandHandler(IEmployeeCommandRepositary commandRepositary,IValidator<Employee> employeeValidator)
        {
            _commandRepositary = commandRepositary;
            _employeeValidator = employeeValidator;
        }

        public async Task<IResponseDataModel<EmployeeDTO>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {          

            try
            {
                var employee = new Employee(request.Employee);
                var validationResult = _employeeValidator.Validate(employee);
                if (validationResult.IsValid == false)
                {
                    return new ResponseDataModel<EmployeeDTO>()
                    {
                        Success = false,
                        Message = string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage))
                    };
                }
                var result = await _commandRepositary.CreateEmployee(employee);
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
    }
}
