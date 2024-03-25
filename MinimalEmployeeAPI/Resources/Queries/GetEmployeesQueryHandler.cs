using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IResponseDataModel<IEnumerable<Employee>>>
    {
        private readonly IEmployeeQueryRepositary _employeeQueryRepositary;
        public GetEmployeesQueryHandler(IEmployeeQueryRepositary employeeQueryRepositary)
        {
            _employeeQueryRepositary = employeeQueryRepositary;
        }

        public async Task<IResponseDataModel<IEnumerable<Employee>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeQueryRepositary.GetEmployees();
        }
    }
}
