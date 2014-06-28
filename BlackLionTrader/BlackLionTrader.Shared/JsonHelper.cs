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
using Windows.Data.Json;

namespace BlackLionTrader
{
    public class JsonHelper
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
        /// <returns>A list of possible Type objects. Returns null if the POST request was unsuccessful.</returns>
        public List<Type> getTypes()
        {
            try
            {
                var result = client.PostAsync("api/v0.9/json/types", null).Result;

                if (result.IsSuccessStatusCode)
                {
                    List<Type> types = new List<Type>();
                    string resultString = result.Content.ReadAsStringAsync().Result;
                    JsonObject jsonObject = JsonObject.Parse(resultString);
                    JsonArray results = jsonObject["results"].GetArray();
                    foreach (JsonValue val in results)
                    {
                        JsonObject tempObject = val.GetObject();
                        List<Subtype> subtypes = new List<Subtype>();
                        foreach (JsonValue subVal in tempObject["subtypes"].GetArray())
                        {
                            JsonObject subObject = subVal.GetObject();
                            subtypes.Add(new Subtype((Int32)subObject["id"].GetNumber(), subObject["name"].GetString()));
                        }
                        types.Add(new Type((Int32)tempObject["id"].GetNumber(), tempObject["name"].GetString(), subtypes));

                    }
                    return types;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the list of rarities and returns the results
        /// </summary>
        /// <returns>A list of possible Rarity objects. Returns null if the POST request was unsuccessful.</returns>
        public List<Rarity> getRarities()
        {
            try
            {
                var result = client.PostAsync("api/v0.9/json/rarities", null).Result;
                if (result.IsSuccessStatusCode)
                {
                    List<Rarity> rarities = new List<Rarity>();
                    string resultString = result.Content.ReadAsStringAsync().Result;
                    JsonObject jsonObject = JsonObject.Parse(resultString);
                    JsonArray results = jsonObject["results"].GetArray();
                    foreach (JsonValue val in results)
                    {
                        JsonObject tempObject = val.GetObject();
                        rarities.Add(new Rarity((Int32)tempObject["id"].GetNumber(), tempObject["name"].GetString()));
                    }
                    return rarities;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the information for the item of the given id
        /// </summary>
        /// <param name="id">The unique id of the item</param>
        /// <returns>An Item object with all it's information. Returns null if POST request was unsuccessful.</returns>
        public Item getItem(int id)
        {
            try
            {
                var result = client.PostAsync("api/v0.9/json/item/" + id, null).Result;
                if (result.IsSuccessStatusCode)
                {
                    string resultString = result.Content.ReadAsStringAsync().Result;
                    JsonObject jsonObject = JsonObject.Parse(resultString);
                    JsonObject itemObject = jsonObject["result"].GetObject();
                    Item item = new Item((Int32)itemObject["data_id"].GetNumber(),
                                         itemObject["name"].GetString(),
                                         (Int32)itemObject["rarity"].GetNumber(),
                                         (Int32)itemObject["restriction_level"].GetNumber(),
                                         itemObject["img"].GetString(),
                                         (Int32)itemObject["type_id"].GetNumber(),
                                         (Int32)itemObject["sub_type_id"].GetNumber(),
                                         itemObject["price_last_changed"].GetString(),
                                         (Int32)itemObject["max_offer_unit_price"].GetNumber(),
                                         (Int32)itemObject["min_sale_unit_price"].GetNumber(),
                                         (Int32)itemObject["offer_availability"].GetNumber(),
                                         (Int32)itemObject["sale_availability"].GetNumber(),
                                         (Int32)itemObject["sale_price_change_last_hour"].GetNumber(),
                                         (Int32)itemObject["offer_price_change_last_hour"].GetNumber()
                                         );
                    return item;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gives a list of possible items with names that match the given searchString
        /// </summary>
        /// <param name="searchString">The item name that is being searched</param>
        /// <returns>The list of possible items</returns>
        public List<Item> searchItem(string searchString)
        {
            try
            {
                int currentPage = 1;
                int totalPages = 1;
                List<Item> items = new List<Item>();
                do
                {
                    var result = client.PostAsync("api/v0.9/json/item-search/" + searchString + "/" + currentPage, null).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string resultString = result.Content.ReadAsStringAsync().Result;
                        JsonObject resultObject = JsonObject.Parse(resultString);
                        if (currentPage == 1)
                        {
                            totalPages = (Int32)resultObject["last_page"].GetNumber();
                        }
                        JsonArray resultsArray = resultObject["results"].GetArray();
                        foreach (JsonValue itemVal in resultsArray)
                        {
                            JsonObject itemObject = itemVal.GetObject();
                            Item item = new Item((Int32)itemObject["data_id"].GetNumber(),
                                                    itemObject["name"].GetString(),
                                                (Int32)itemObject["rarity"].GetNumber(),
                                                (Int32)itemObject["restriction_level"].GetNumber(),
                                                itemObject["img"].GetString(),
                                                (Int32)itemObject["type_id"].GetNumber(),
                                                (Int32)itemObject["sub_type_id"].GetNumber(),
                                                itemObject["price_last_changed"].GetString(),
                                                (Int32)itemObject["max_offer_unit_price"].GetNumber(),
                                                (Int32)itemObject["min_sale_unit_price"].GetNumber(),
                                                (Int32)itemObject["offer_availability"].GetNumber(),
                                                (Int32)itemObject["sale_availability"].GetNumber(),
                                                (Int32)itemObject["sale_price_change_last_hour"].GetNumber(),
                                                (Int32)itemObject["offer_price_change_last_hour"].GetNumber()
                            );
                            items.Add(item);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    currentPage++;
                } while (currentPage <= totalPages);
                return items;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the current conversation rates between gold and gems
        /// </summary>
        /// <returns>A GemPrice object. Returns null if POST request was unsuccessful</returns>
        public GemPrice getGemPrices()
        {
            try
            {
                var result = client.PostAsync("api/v0.9/json/gem-price", null).Result;
                if (result.IsSuccessStatusCode)
                {
                    string resultString = result.Content.ReadAsStringAsync().Result;
                    JsonObject jsonObject = JsonObject.Parse(resultString);
                    JsonObject gemObject = jsonObject["result"].GetObject();
                    GemPrice gemPrice = new GemPrice((Int32)gemObject["gem_to_gold"].GetNumber(), (Int32)gemObject["gold_to_gem"].GetNumber());
                    return gemPrice;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
