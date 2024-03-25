using EmployeeAPI.ResponseModels;
using MediatR;

namespace EmployeeAPI.Resources.Commands
{
    public class DeleteEmployeeCommand : IRequest<IResponseModel>
    {
        public int Id { get; set; }
    }
}
