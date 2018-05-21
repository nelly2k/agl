using System;
using agl.app;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace agl.ui
{
    class Program
    {
        static void Main(string[] args)
        {
            Main().Wait();
        }

        static async Task Main(){
            var people = await Fetch();
            var genderCatDict = Convert(people);
            Display(genderCatDict);
        }

        static async Task<ICollection<Human>> Fetch(){
            var fetcher = new PeopleFetcher();
            return await fetcher.Execute();
        }

        static Dictionary<string, IEnumerable<string>> Convert(ICollection<Human> people){
            var  catGenderBuilder = new CatGenderBuilder();
            return catGenderBuilder.Execute(people);
        }

        static void Display(Dictionary<string, IEnumerable<string>> gendersCat){
            foreach (var gender in gendersCat)
            {
                Console.WriteLine(gender.Key);

                foreach(var petName in gender.Value){
                    Console.WriteLine($"\t{petName}");
                }
            }
        }
    }
}
