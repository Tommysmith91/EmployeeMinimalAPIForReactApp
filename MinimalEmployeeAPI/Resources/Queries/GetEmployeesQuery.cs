using EmployeeAPI.Entities;
using EmployeeAPI.ResponseModels;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeesQuery : IRequest<IResponseDataModel<IEnumerable<Employee>>>
    {
    }
}
