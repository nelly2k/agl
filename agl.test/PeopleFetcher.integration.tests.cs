using System.Threading.Tasks;
using agl.app;
using Xunit;

namespace agl.test
{
    public class PeopleFetcherIntegrationTests
    {
        PeopleFetcher peopleFetcher;
        public PeopleFetcherIntegrationTests()
        {
            peopleFetcher = new PeopleFetcher();
        }

        [Fact]
        public async Task Fetches_Data(){
            var result = await peopleFetcher.Execute();
            Assert.NotEmpty(result);
        }
    }
}