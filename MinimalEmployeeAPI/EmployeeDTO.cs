using EmployeeAPI.Models;

namespace EmployeeAPI
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool HasRightToWork { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string CityTown { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public EmployeeDTO() { }
        public EmployeeDTO(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Age = employee.Age;
            HasRightToWork = employee.HasRightToWork;
            AddressLine1= employee.AddressLine1;
            AddressLine2= employee.AddressLine2;
            CityTown= employee.CityTown;
            Postcode= employee.Postcode;
            Country= employee.Country;
            StartOfEmployment= employee.StartOfEmployment; 
        }        
    }
}
