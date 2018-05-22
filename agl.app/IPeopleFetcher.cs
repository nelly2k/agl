using System.Collections.Generic;
using System.Threading.Tasks;

namespace agl.app
{
    public interface IPeopleFetcher
    {
        Task<ICollection<Person>> Execute(string connectionString);
    }
}