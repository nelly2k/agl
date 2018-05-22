using System.Threading.Tasks;
using agl.app;
using Xunit;

namespace agl.test
{
    public class PeopleFetcherIntegrationTests
    {
        private readonly PeopleFetcher _peopleFetcher;
        public PeopleFetcherIntegrationTests()
        {
            _peopleFetcher = new PeopleFetcher();
        }

        [Fact]
        public async Task Fetches_Data(){
            var result = await _peopleFetcher.Execute("http://agl-developer-test.azurewebsites.net/people.json");
            Assert.NotEmpty(result);
        }
    }
}