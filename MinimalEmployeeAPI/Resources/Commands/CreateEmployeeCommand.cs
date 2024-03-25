using EmployeeAPI.ResponseModels;
using MediatR;

namespace EmployeeAPI.Resources.Commands
{
    public class CreateEmployeeCommand : IRequest<IResponseDataModel<EmployeeDTO>>
    {
        public EmployeeDTO Employee { get; set; }
    }
}
