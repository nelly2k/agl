using System;
using agl.app;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace agl.ui
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            MainAsync().Wait();
        }

        private static async Task MainAsync(){
           
            var appControoler = _serviceProvider.GetService<IAppController>();

            var genderCatDict = await appControoler.Execute();
            Display(genderCatDict);
            Console.ReadKey();
        }

        private static void Display(Dictionary<string, IEnumerable<string>> gendersCat){
            foreach (var gender in gendersCat)
            {
                Console.WriteLine(gender.Key);

                foreach(var petName in gender.Value){
                    Console.WriteLine($"\t{petName}");
                }
            }
        }

        private static void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(new AppConfiguration{ConnectionString = configuration.GetSection("People:ConnectionString").Value });
          
            services.AddTransient<IPeopleFetcher, PeopleFetcher>();
            services.AddTransient<ICatGenderBuilder, CatGenderBuilder>();
            services.AddTransient<IAppController, AppController>();
        }
    }
}
