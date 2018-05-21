using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace agl.app{
    
    public  class PeopleFetcher{
        public async Task<ICollection<Human>> Execute(){

            using(var client = new HttpClient()){
                var response = await client.GetAsync("http://agl-developer-test.azurewebsites.net/people.json");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<ICollection<Human>>();
            }
        }
    }
}