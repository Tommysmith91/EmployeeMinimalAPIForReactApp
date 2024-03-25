using EmployeeAPI;
using EmployeeAPI.Abstractions;
using EmployeeAPI.Concrete;
using EmployeeAPI.DataSeeder;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMinimalAPI.Tests.Abstractions
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>,IDisposable
    {
        private readonly IServiceScope _scope;
        protected EmployeeDb _dbContext { get; }
        protected ISeedFaker _seedFaker { get; }
        protected IEmployeeCommandRepositary _employeeCommandRepositary { get; }
        protected IEmployeeQueryRepositary _employeeQueryRepositary { get; }

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<EmployeeDb>();
            _seedFaker = _scope.ServiceProvider.GetRequiredService<ISeedFaker>();
            _employeeCommandRepositary = _scope.ServiceProvider.GetRequiredService<IEmployeeCommandRepositary>();
            _employeeQueryRepositary = _scope.ServiceProvider.GetRequiredService<IEmployeeQueryRepositary>();

        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
