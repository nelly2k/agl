using System.Collections.Generic;

namespace agl.app
{
    public class Human
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}