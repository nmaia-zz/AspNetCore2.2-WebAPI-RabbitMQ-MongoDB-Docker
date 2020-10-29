using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace Demo.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection 
        : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable
        where TStartup : class
    {
        public readonly DemographicCensusFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions 
            {
                BaseAddress = new Uri("http://localhost")
            };

            Factory = new DemographicCensusFactory<TStartup>(); // Emulating a Web Application in memory
            Client = Factory.CreateClient(clientOptions); // HttpClient to handle application behaviors
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
