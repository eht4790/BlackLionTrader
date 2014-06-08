/*
 * JsonHelper.cs
 * 
 * Stores the HttpClient object which POSTs to gw2spidy.com
 * and parses the results.
 * 
 * Copyright (C) 2014  Eric Trumble https://github.com/eht4790
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * */

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
