using EmployeeAPI.ResponseModels;
using MediatR;

namespace EmployeeAPI.Resources.Commands
{
    public class UpdateEmployeeCommand : IRequest<IResponseModel>
    {
        public EmployeeDTO Employee { get; set; }
        public int Id { get; set; }
    }
}
