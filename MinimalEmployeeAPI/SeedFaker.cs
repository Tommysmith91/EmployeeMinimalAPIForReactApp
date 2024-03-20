using EmployeeAPI.Abstractions;
using EmployeeAPI.Models;

namespace EmployeeAPI
{
    public class SeedFaker : ISeedFaker
    {
        private readonly EmployeeDb _context;
        const int MAX_NUMBER_OF_FAKE_ENTRIES = 10;
        const int MIN_AGE = 1;
        const int MAX_AGE = 110;
        public SeedFaker(EmployeeDb context)
        {
            _context = context;
        }
        public void Initialise()
        {
            if(_context.Employees.Any() == false)
            {
                _context.Employees.AddRange(CreateSomeDummyData());
                _context.SaveChanges();
            }
        }
        private List<Employee> CreateSomeDummyData()
        {
            var employees = new List<Employee>();
            for (var noOfEmployeesToCreate = 1; noOfEmployeesToCreate <= MAX_NUMBER_OF_FAKE_ENTRIES; ++noOfEmployeesToCreate)
            {
                employees.Add(new Employee
                {                    
                    AddressLine1 = $"{Faker.LocationFaker.StreetNumber()} {Faker.LocationFaker.StreetName()}",
                    CityTown = Faker.LocationFaker.City(),
                    Name = Faker.NameFaker.Name(),
                    Age = Faker.NumberFaker.Number(MIN_AGE, MAX_AGE),
                    Country = Faker.LocationFaker.Country(),
                    Postcode = Faker.LocationFaker.PostCode(),
                    HasRightToWork = Faker.BooleanFaker.Boolean(),
                    StartOfEmployment = Faker.DateTimeFaker.DateTime()
                });
            }
            return employees;
        }
    }
}
