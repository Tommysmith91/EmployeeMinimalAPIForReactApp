using MediatR;

namespace EmployeeAPI.Resources.Commands
{
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
