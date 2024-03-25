using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeeQuery : IRequest<IResponseDataModel<Employee>>
    {
        public int Id { get; set; }
    }
}
