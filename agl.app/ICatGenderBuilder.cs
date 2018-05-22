using System.Collections.Generic;

namespace agl.app
{
    public interface ICatGenderBuilder
    {
        Dictionary<string, IEnumerable<string>> Execute(IEnumerable<Person> people);
    }
}