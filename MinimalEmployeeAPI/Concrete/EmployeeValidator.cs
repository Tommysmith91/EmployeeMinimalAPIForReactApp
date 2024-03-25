using EmployeeAPI.Entities;
using FluentValidation;

namespace EmployeeAPI.Concrete
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator() 
        { 
          RuleFor(employee => employee.Name).NotNull().NotEmpty();
          RuleFor(employee => employee.Age).NotNull().GreaterThan(0);
          RuleFor(employee => employee.Postcode).NotNull().NotEmpty();
          RuleFor(employee => employee.AddressLine1).NotNull().NotEmpty();
        }

    }
}
