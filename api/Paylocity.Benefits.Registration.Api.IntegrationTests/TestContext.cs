using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Paylocity.Benefits.Registration.IntegrationTests
{
    public class TestContext : IDisposable
    {
        private TestServer _testServer;

        public HttpClient Client { get; private set; }

        public TestContext()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            _testServer?.Dispose();
            Client?.Dispose();
        }
    }
}
