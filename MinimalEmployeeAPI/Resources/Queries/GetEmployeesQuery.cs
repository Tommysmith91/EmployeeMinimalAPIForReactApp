using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeesQuery : IRequest<IResponseDataModel<IEnumerable<Employee>>>
    {
    }
}
