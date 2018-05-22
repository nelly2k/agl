using System.Collections.Generic;
using System.Threading.Tasks;

namespace agl.app
{
    public interface IAppController
    {
        Task<Dictionary<string, IEnumerable<string>>> Execute();
    }
}