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

            Console.WriteLine("Make numeric input (integer):");
            int input;

            if(int.TryParse(Console.ReadLine(), out input))
            {
                Task.Run(async () =>
                {
                    ICollection<Starship> data = await client.GetStarshipData();
                    handler.DisplayStops(input, data.ToList());
                }).ConfigureAwait(false);
            }

            else
            {
                Console.WriteLine("Your input is not valid.");
            }

            Console.ReadKey();
        }
    }
}
