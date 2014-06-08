/// JsonHelper.cs
/// 
/// Stores the HttpClient object which POSTs to gw2spidy.com
/// and parses the results.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BlackLionTrader
{
    class JsonHelper
    {
        /// <summary>
        /// The HttpClient that POSTs to gw2spidy.com
        /// </summary>
        private HttpClient client = new HttpClient();

        public JsonHelper()
        {
            client.BaseAddress = new Uri("http://www.gw2spidy.com");
        }

        /// <summary>
        /// Gets the available item types and returns the parsed results.
        /// </summary>
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
