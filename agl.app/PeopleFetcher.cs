using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace agl.app
{
    public class PeopleFetcher : IPeopleFetcher
    {
        public async Task<ICollection<Person>> Execute(string connectionString)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(connectionString);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<ICollection<Person>>();
            }
        }
    }
}