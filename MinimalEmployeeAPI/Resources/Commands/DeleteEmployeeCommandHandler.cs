using EmployeeAPI.Abstractions;
using EmployeeAPI.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EmployeeAPI.Resources.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, IResponseModel>
    {
        private readonly IEmployeeCommandRepositary _commandRepositary;
        public DeleteEmployeeCommandHandler(IEmployeeCommandRepositary commandRepositary) 
        { 
            _commandRepositary = commandRepositary;
        }
        public async Task<IResponseModel> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _commandRepositary.DeleteEmployee(request.Id);
        }
    }
}
