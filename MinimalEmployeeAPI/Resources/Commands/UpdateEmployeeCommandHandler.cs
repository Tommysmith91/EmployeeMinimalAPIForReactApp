using EmployeeAPI.Abstractions;
using EmployeeAPI.Entities;
using EmployeeAPI.Models;
using EmployeeAPI.ResponseModels;
using FluentValidation;
using MediatR;

namespace EmployeeAPI.Resources.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IResponseModel>
    {
        private readonly IEmployeeCommandRepositary _commandRepositary;
        private readonly IValidator<Employee> _employeeValidator;
        public UpdateEmployeeCommandHandler(IEmployeeCommandRepositary commandRespositary, IValidator<Employee> employeeValidator)
        {
            _commandRepositary = commandRespositary;
            _employeeValidator = employeeValidator;
        }
        public async Task<IResponseModel> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dbEntity = new Employee(request.Employee);
                var validationResult = _employeeValidator.Validate(dbEntity);
                if (validationResult.IsValid == false)
                {
                    return new ResponseModel
                    {
                        Success = false
                    };
                }
                var result = await _commandRepositary.UpdateEmployee(dbEntity, request.Id);
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
