using Microsoft.Extensions.DependencyInjection;
using SWAPIStop.Data;
using SWAPIStop.Models;
using SWAPIStop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWAPIStop
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddHttpClient();
            collection.AddTransient<StoppingHandler>();
            collection.AddTransient<SWAPIClient>();

            var serviceProvider = collection.BuildServiceProvider();
            var client = serviceProvider.GetService<SWAPIClient>();
            var handler = serviceProvider.GetService<StoppingHandler>();

            var input = Convert.ToInt32(Console.ReadLine());

            Task.Run(async () =>
            {
                ICollection<Starship> data = await client.GetStarshipData();
                handler.DisplayStops(input, data.ToList());
            }).ConfigureAwait(false);

            Console.ReadKey();
        }
    }
}
