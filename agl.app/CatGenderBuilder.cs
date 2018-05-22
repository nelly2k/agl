using System.Collections.Generic;
using System.Linq;

namespace agl.app
{
    public class CatGenderBuilder : ICatGenderBuilder
    {
        public Dictionary<string, IEnumerable<string>> Execute(IEnumerable<Person> people)
        {
            var result = new Dictionary<string, IEnumerable<string>>();

            foreach (var person in people)
            {
                var petNames = person.Pets?
                    .Where(x => x.Type == "Cat").Select(x => x.Name)
                    .ToList();

                if (petNames == null || !petNames.Any())
                {
                    continue;
                }
                if (!result.ContainsKey(person.Gender))
                {
                    result.Add(person.Gender, petNames.OrderBy(x => x));
                }
                else
                {
                    result[person.Gender] = result[person.Gender].Concat(petNames).OrderBy(x => x);
                }

            }
            return result;
        }
    }
}