using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace BlackLionTrader
{
    class SearchModel
    {
        private HttpClient client = new HttpClient();

        private List<string> types = new List<string>();

        public List<string> Types
        {
            get { return types; }
        }

        private List<string> rarities = new List<string>();

        public List<string> Rarities
        {
            get { return rarities; }
        }
        public SearchModel()
        {
            client.BaseAddress = new Uri("http://www.gw2spidy.com");
            setup();
        }

        private void setup()
        {
            var result = client.PostAsync("api/v0.9/json/types", null).Result;
            if (result.IsSuccessStatusCode)
            {
                string resultContent = result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
