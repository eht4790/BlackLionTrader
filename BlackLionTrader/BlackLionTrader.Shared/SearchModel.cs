/*
 * SearchModel.cs
 * 
 * Tracks thes tate of the Search hub section
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
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace BlackLionTrader
{
    public class SearchModel
    {
        private JsonHelper jsonHelper;
        private List<Type> types = new List<Type>();
        private List<Subtype> subtypes = new List<Subtype>();
        private List<Rarity> rarities = new List<Rarity>();
        private Dictionary<int, Item> searchResults = new Dictionary<int, Item>();

        private Type currentType = null;
        private Subtype currentSubtype = null;
        private Rarity currentRarity = null;

        private int minLvl = 0;
        private int maxLvl = 80;

        private enum Columns { Name, Lvl, Supply, Demand, MinSale, MaxBuy, Margin };
        private Columns currentSortColumn = Columns.Name;
        private bool sortAscending = true;

        /// <summary>
        /// A list of available types to filter search results
        /// </summary>
        public List<Type> Types
        {
            get { return types; }
        }

        /// <summary>
        /// A list of available subtypes of the selected type to filter search results
        /// </summary>
        public List<Subtype> SubTypes
        {
            get { return subtypes; }
        }

        /// <summary>
        /// A list of available rarity levels to filter search results
        /// </summary>
        public List<Rarity> Rarities
        {
            get { return rarities; }
        }

        /// <summary>
        /// The minimum level of an item to filter search results
        /// </summary>
        public int MinLvl
        {
            get { return minLvl; }
        }

        /// <summary>
        /// The maximum level of an item to filter search results
        /// </summary>
        public int MaxLvl
        {
            get { return maxLvl; }
        }

        public SearchModel(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
            try
            {
                setup();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Initial setup of data members
        /// </summary>
        private void setup()
        {
            try
            {
                types = jsonHelper.getTypes();
                rarities = jsonHelper.getRarities();
                rarities.Insert(0, new Rarity(-1, "Any"));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Set currentType to the Type of the given id and updates the subtypes list.
        /// If id equals -1 restore to default.
        /// </summary>
        /// <param name="id">The id of the new currently selected Type</param>
        public void changeType(int id)
        {
            if (id == -1)
            {
                currentType = null;
                currentSubtype = null;
                subtypes = new List<Subtype>();
            }
            else
            {
                currentType = types[id];
                subtypes = currentType.Subtypes;
            }
        }

        /// <summary>
        /// Set currentSubtype to the Subtype of the given id.
        /// If id equals - 1 restore to default.
        /// </summary>
        /// <param name="id">The id of the new currently selected Subtype</param>
        public void changeSubtype(int id)
        {
            if (id == -1)
            {
                currentSubtype = null;
            }
            else
            {
                currentSubtype = subtypes[id];
            }
        }

        /// <summary>
        /// Set currentRarity to the Rarity of the given id.
        /// If id equals -1 restore to default.
        /// </summary>
        /// <param name="id"></param>
        public void changeRarity(int id)
        {
            if(id == 0)
            {
                currentRarity = null;
            }
            else
            {
                currentRarity = rarities[id];
            }
        }

        /// <summary>
        /// Sets the minimum level to the given value. If the given value 
        /// is less than 1 it defaults to 1. If greater than the max lvl it
        /// defaults to match the maxLvl;
        /// </summary>
        /// <param name="lvl">The desired value for minLvl</param>
        public void changeMinLvl(int lvl)
        {
            if(lvl > 0)
            {
                if(lvl <= maxLvl)
                {
                    minLvl = lvl;
                }
                else
                {
                    minLvl = maxLvl;
                }
            }
            else
            {
                minLvl = 1;
            }
        }

        /// <summary>
        /// Sets the maximum level to the given value. If the given value 
        /// is greater than 80 it defaults to 80. If less than the min lvl it
        /// defaults to match the minLvl;
        /// </summary>
        /// <param name="lvl">The desired value for minLvl</param>
        public void changeMaxLvl(int lvl)
        {
            if(lvl <= 80)
            {
                if(lvl >= minLvl)
                {
                    maxLvl = lvl;
                }
                else
                {
                    maxLvl = minLvl;
                }
            }
            else
            {
                maxLvl = 80;
            }
        }

        /// <summary>
        /// Searches for items with the given name, filters the results
        /// according to search options, and store the results in searchResults
        /// </summary>
        /// <param name="itemName">The item name being searched</param>
        public async Task search(string itemName)
        {
            try
            {
                searchResults.Clear();
                List<Item> results = await jsonHelper.searchItem(itemName);
                if (results == null)
                {
                    //TODO: Message Dialog to user
                }
                var linqResults = from item in results
                                  where item.RestrictionLevel >= minLvl &&
                                        item.RestrictionLevel <= maxLvl &&
                                        item.SaleAvailability > 0 &&
                                        item.OfferAvailability > 0
                                  select item;

                if (currentType != null)
                {
                    linqResults = linqResults.Where(item => item.TypeId == currentType.ID);
                }

                if (currentSubtype != null)
                {
                    linqResults = linqResults.Where(item => item.SubtypeId == currentSubtype.ID);
                }

                if (currentRarity != null)
                {
                    linqResults = linqResults.Where(item => item.RarityId == currentRarity.ID);
                }

                foreach(Item item in linqResults)
                {
                    if (!searchResults.ContainsKey(item.ID))
                    {
                        searchResults.Add(item.ID, item);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Searches for all items of the given type id, filters them, and stores
        /// them in searchResults
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public async Task searchAll()
        {
            searchResults.Clear();
            if (currentType != null)
            {
                try
                {
                    List<Item> results = await jsonHelper.searchAll(currentType.ID);
                    if (results == null)
                    {
                        //TODO: Message Dialo to user
                    }
                    var linqResults = from item in results
                                      where item.RestrictionLevel >= minLvl &&
                                            item.RestrictionLevel <= maxLvl &&
                                            item.SaleAvailability > 0 &&
                                            item.OfferAvailability > 0
                                      select item;

                    if (currentSubtype != null)
                    {
                        linqResults = linqResults.Where(item => item.SubtypeId == currentSubtype.ID);
                    }

                    if (currentRarity != null)
                    {
                        linqResults = linqResults.Where(item => item.RarityId == currentRarity.ID);
                    }

                    foreach (Item item in linqResults)
                    {
                        searchResults.Add(item.ID, item);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets a list of DisplayItems from the searchResults list
        /// </summary>
        /// <returns>List of DisplayItem objects that match current search</returns>
        public List<DisplayItem> getDisplayItems()
        {
            List<DisplayItem> displayItems = new List<DisplayItem>();
            foreach(Item item in searchResults.Values)
            {
                displayItems.Add(new DisplayItem(item));
            }
            List<DisplayItem> sortedList = new List<DisplayItem>();
            switch(currentSortColumn)
            {
                case Columns.Name:
                    sortedList = displayItems.OrderBy(item => item.Name).ToList();
                    break;

                case Columns.Lvl:
                    sortedList = displayItems.OrderBy(item => item.Level).ToList();
                    break;

                case Columns.Supply:
                    sortedList = displayItems.OrderBy(item => item.Supply).ToList();
                    break;

                case Columns.Demand:
                    sortedList = displayItems.OrderBy(item => item.Demand).ToList();
                    break;

                case Columns.MinSale:
                    sortedList = displayItems.OrderBy(item => Int32.Parse(item.MinSaleOffer)).ToList();
                    break;

                case Columns.MaxBuy:
                    sortedList = displayItems.OrderBy(item => Int32.Parse(item.MaxBuyOffer)).ToList();
                    break;

                case Columns.Margin:
                    sortedList = displayItems.OrderBy(item => Int32.Parse(item.Margin)).ToList();
                    break;

                default:
                    break;
            }
            if(!sortAscending)
            {
                sortedList.Reverse();
            }

            return sortedList;
        }

        /// <summary>
        /// Changes the column that items are sorted by
        /// </summary>
        /// <param name="id">The int value of a corresponding enum in Columns</param>
        public void changeSortColumn(int id)
        {
            switch(id)
            {
                case (int)Columns.Name:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.Name;
                    }
                    break;

                case (int)Columns.Lvl:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.Lvl;
                    }
                    break;

                case (int)Columns.Supply:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.Supply;
                    }
                    break;

                case (int)Columns.Demand:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.Demand;
                    }
                    break;

                case (int)Columns.MinSale:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.MinSale;
                    }
                    break;

                case (int)Columns.MaxBuy:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.MaxBuy;
                    }
                    break;

                case (int)Columns.Margin:
                    if((int)currentSortColumn == id)
                    {
                        sortAscending = !sortAscending;
                    }
                    else
                    {
                        currentSortColumn = Columns.Margin;
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Returns the Item object with the given id
        /// </summary>
        /// <param name="id">The id of the desired item in the list results</param>
        /// <returns>The Item object that corresponds to the given id</returns>
        public Item getItem(int id)
        {
            return searchResults[id];
        }
    }
}
