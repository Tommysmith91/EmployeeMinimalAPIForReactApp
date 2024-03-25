using EmployeeAPI.Entities;
using EmployeeAPI.Resources;
using EmployeeMinimalAPI.Tests.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var request = CreateSuccessfulEmployeeRequest();
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/employees", request);
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.Created);          
        }
        [Fact]
        public async Task ShouldReturn400BadRequestStatusCodeOnFailedCreation()
        {
            //Arrange
            var request = CreateBadEmployeeRequestWithNoNameAndZeroAge();
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/employees", request);
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task ShouldReturnErrorMessagesOnBadRequest()
        {
            //Arrange
            var request = CreateBadEmployeeRequestWithNoNameAndZeroAge();
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/employees", request);
            var jsonContent = await response.Content.ReadAsStringAsync();            
            
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
            jsonContent.Should().NotBeEmpty();
            jsonContent.Should().ContainAny(new List<string> { "Age", "Name" });      
            
        }

        [Fact]
        public async Task ShouldReturnObjectThatIsEqualToRequestObjectProvided()
        {
            //Arrange
            var request = CreateSuccessfulEmployeeRequest();
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

        private Employee CreateSuccessfulEmployeeRequest()
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
        private Employee CreateBadEmployeeRequestWithNoNameAndZeroAge()
        {
            return new Employee
            {
                AddressLine1 = $"{Faker.LocationFaker.StreetNumber()} {Faker.LocationFaker.StreetName()}",
                CityTown = Faker.LocationFaker.City(),
                Name = "",
                Age = 0,
                Country = Faker.LocationFaker.Country(),
                Postcode = Faker.LocationFaker.PostCode(),
                HasRightToWork = Faker.BooleanFaker.Boolean(),
                StartOfEmployment = Faker.DateTimeFaker.DateTime()
            };
        }
    }
}