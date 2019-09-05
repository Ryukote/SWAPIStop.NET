using Microsoft.Extensions.DependencyInjection;
using SWAPIStop.Data;
using SWAPIStop.Models;
using SWAPIStop.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System;

namespace SWAPIStop.Test
{
    public class SWAPIClientTest
    {
        ServiceCollection collection;
        ServiceProvider provider;
        public SWAPIClientTest()
        {
            collection = new ServiceCollection();
            collection.AddHttpClient();
            collection.AddTransient<StoppingHandler>();
            provider = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task WillReturnCollectionWith37Items()
        {
            var client = new SWAPIClient(provider.GetService<IHttpClientFactory>());

            var result = await client.GetStarshipData();

            Assert.NotNull(result);
            Assert.True(result.Count == 37);
        }

        [Fact]
        public async Task WillReturnConsumablesAsDays()
        {
            var client = new SWAPIClient
                (provider.GetService<IHttpClientFactory>());
            var handler = new StoppingHandler();

            var result = await client.GetStarshipData();

            Assert.NotNull(result);
            Assert.Equal(2190, handler.PeriodToDays(result.ToList()[0].Consumables));
        }

        [Fact]
        public async Task WillGetNumberOfStops()
        {
            var client = new SWAPIClient(provider.GetService<IHttpClientFactory>());
            var handler = new StoppingHandler();

            var result = await client.GetStarshipData();

            //Millennium Falcon: 9
            //Index in collection: 3
            var milleniumFalcon = result.ToList()[3];
            var convertedConsumables = handler.PeriodToDays(milleniumFalcon.Consumables);
            var milleniumFalconStops = handler.NumberOfStops(1000000, Convert.ToInt32(milleniumFalcon.MGLT), convertedConsumables);

            Assert.NotNull(result);
            Assert.Equal(9, milleniumFalconStops);            
        }
    }
}
