using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlackLionTrader
{
    class JsonHelper
    {
        private HttpClient client = new HttpClient();

        public JsonHelper()
        {
            client.BaseAddress = new Uri("http://www.gw2spidy.com");
        }

        public void getTypes()
        {
            var result = client.PostAsync("api/v0.9/json/types", null).Result;
            if (result.IsSuccessStatusCode)
            {
                string resultContent = result.Content.ReadAsStringAsync().Result;
            }
        }

    }
}
