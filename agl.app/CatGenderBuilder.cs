using System.Collections.Generic;
using System.Linq;

namespace agl.app
{
    public class CatGenderBuilder
    {
        public Dictionary<string, IEnumerable<string>> Execute(IEnumerable<Human> humans)
        {
            var result = new Dictionary<string, IEnumerable<string>>();

            foreach (var human in humans)
            {
                var petNames = human.Pets?.Where(x => x.Type == "Cat").Select(x => x.Name);
                if (petNames == null || !petNames.Any())
                {
                    continue;
                }
                if (!result.ContainsKey(human.Gender))
                {
                    result.Add(human.Gender, petNames.OrderBy(x => x));
                }
                else
                {
                    result[human.Gender] = result[human.Gender].Concat(petNames).OrderBy(x => x);
                }

            }
            return result;
        }
    }
}