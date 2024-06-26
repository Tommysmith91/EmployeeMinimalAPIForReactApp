﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeAPI.Resources;

namespace EmployeeAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string CityTown { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool HasRightToWork { get; set; }

        public Employee()
        {

        }
        public Employee(EmployeeDTO employeeDTO)
        {
            Id = employeeDTO.Id;
            Name = employeeDTO.Name;
            Age = employeeDTO.Age;
            StartOfEmployment = employeeDTO.StartOfEmployment;
            AddressLine1 = employeeDTO.AddressLine1;
            AddressLine2 = employeeDTO.AddressLine2;
            CityTown = employeeDTO.CityTown;
            Postcode = employeeDTO.Postcode;
            Country = employeeDTO.Country;
            HasRightToWork = employeeDTO.HasRightToWork;
        }
    }
}
