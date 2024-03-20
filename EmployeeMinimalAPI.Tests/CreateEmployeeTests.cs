using EmployeeAPI;
using EmployeeAPI.Models;
using EmployeeMinimalAPI.Tests.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;

namespace EmployeeMinimalAPI.Tests
{
    public class CreateEmployeeTests : BaseIntegrationTest
    {
        private readonly IntegrationTestWebAppFactory _factory;

        public CreateEmployeeTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturn201CreatedStatusCodeOnSuccessfulCreation()
        {
            //Arrange
            var request = CreateEmployeeRequest();
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/employees", request);
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.Created);          
        }

        [Fact]
        public async Task ShouldReturnObjectThatIsEqualToRequestObjectProvided()
        {
            //Arrange
            var request = CreateEmployeeRequest();
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/employees", request);            
            //Assert            
            var resultObject = await response.Content.ReadFromJsonAsync<EmployeeDTO>();

            resultObject.Id.Should().BeGreaterThan(0);
            resultObject.Name.Should().Be(request.Name);
            resultObject.Age.Should().Be(request.Age);
            resultObject.AddressLine1.Should().Be(request.AddressLine1);
            resultObject.AddressLine2.Should().Be(request.AddressLine2);
            resultObject.Postcode.Should().Be(request.Postcode);
            resultObject.Country.Should().Be(request.Country);
            resultObject.CityTown.Should().Be(request.CityTown);
        }

        private Employee CreateEmployeeRequest()
        {
            return new Employee
            {
                AddressLine1 = $"{Faker.LocationFaker.StreetNumber()} {Faker.LocationFaker.StreetName()}",
                CityTown = Faker.LocationFaker.City(),
                Name = Faker.NameFaker.Name(),
                Age = Faker.NumberFaker.Number(1, 100),
                Country = Faker.LocationFaker.Country(),
                Postcode = Faker.LocationFaker.PostCode(),
                HasRightToWork = Faker.BooleanFaker.Boolean(),
                StartOfEmployment = Faker.DateTimeFaker.DateTime()
            };
        }
    }
}