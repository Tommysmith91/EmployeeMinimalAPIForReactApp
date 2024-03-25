using EmployeeAPI.Abstractions;
using EmployeeAPI.Concrete;
using EmployeeAPI.Models;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, IResponseDataModel<Employee>>
    {
        private readonly IEmployeeQueryRepositary _employeeQueryRepositary;
        public GetEmployeeQueryHandler(IEmployeeQueryRepositary employeeQueryRepositary) 
        {
            _employeeQueryRepositary = employeeQueryRepositary;
        }

        public Task<IResponseDataModel<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            return _employeeQueryRepositary.GetEmployee(request.Id);
        }
    }
}
