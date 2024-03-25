using EmployeeAPI.Entities;
using EmployeeAPI.ResponseModels;
using MediatR;

namespace EmployeeAPI.Resources.Queries
{
    public class GetEmployeeQuery : IRequest<IResponseDataModel<Employee>>
    {
        public int Id { get; set; }
    }
}
