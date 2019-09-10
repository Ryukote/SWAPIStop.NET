using Newtonsoft.Json;
using SWAPIStop.Constants;
using SWAPIStop.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SWAPIStop.Utilities
{
    public class SWAPIClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public SWAPIClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ICollection<Starship>> GetStarshipData()
        {
            var client = _clientFactory.CreateClient();

            List<Starship> starshipCollection = new List<Starship>();

            HttpResponseMessage response;

            for(int counter = 1; ;counter++)
            {
                response = await client.GetAsync(Url.GetStarships + counter);

                if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SWAPIJson>(json);

                    foreach (var item in result.Result)
                    {
                        starshipCollection.Add(item);
                    }
                }

                else
                {
                    break;
                }
            }

            return starshipCollection;
        }
    }
}
