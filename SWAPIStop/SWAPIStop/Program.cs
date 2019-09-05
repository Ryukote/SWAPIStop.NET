using Microsoft.Extensions.DependencyInjection;
using SWAPIStop.Utilities;
using System;
using System.Threading.Tasks;

namespace SWAPIStop
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddHttpClient();
            collection.AddTransient<SWAPIClient>();

            var serviceProvider = collection.BuildServiceProvider();
            var service = serviceProvider.GetService<SWAPIClient>();

            Task.Run(async () => await service.GetStarshipData());

            Console.ReadKey();
        }
    }
}
