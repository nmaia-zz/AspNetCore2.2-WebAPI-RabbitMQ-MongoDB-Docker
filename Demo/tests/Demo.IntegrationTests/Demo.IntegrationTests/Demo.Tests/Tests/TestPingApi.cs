using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests.Tests
{
    [Collection("Integration tests collection")]
    public class TestPingApi
    {
        private readonly TestHostFixture _testHostFixture;

        public TestPingApi(TestHostFixture testHostFixture)
        {
            _testHostFixture = testHostFixture;
        }

        [Theory]
        [InlineData("/api/tests/ping")]
        public async Task Returns_OK(string endpoint)
        {
            var response = await _testHostFixture.Client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
