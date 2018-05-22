using System.Collections.Generic;
using System.Threading.Tasks;

namespace agl.app
{
    public class AppController : IAppController
    {
        private readonly IPeopleFetcher _peopleFetcher;
        private readonly ICatGenderBuilder _catGenderBuilder;
        private readonly AppConfiguration _configuration;

        public AppController(IPeopleFetcher peopleFetcher,
            ICatGenderBuilder catGenderBuilder,
            AppConfiguration configuration)
        {
            _peopleFetcher = peopleFetcher;
            _catGenderBuilder = catGenderBuilder;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, IEnumerable<string>>> Execute()
        {
            var connectionString = _configuration.ConnectionString;
            var people = await _peopleFetcher.Execute(connectionString);
            var gendersWithCats = _catGenderBuilder.Execute(people);
            return gendersWithCats;
        }
    }
}
