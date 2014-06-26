﻿/*
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
        private Dictionary<int, Type> typesDict = new Dictionary<int, Type>();
        private List<Subtype> subtypes = new List<Subtype>();
        private List<Rarity> rarities = new List<Rarity>();
        private List<Item> searchResults = new List<Item>();

        private Type currentType = null;
        private Subtype currentSubtype = null;
        private Rarity currentRarity = null;

        private int minLvl = 0;
        private int maxLvl = 80;

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
        /// A list of items that match the search parameters
        /// </summary>
        public List<Item> SearchResults
        {
            get { return searchResults; }
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
            setup();
        }

        /// <summary>
        /// Initial setup of data members
        /// </summary>
        private void setup()
        {
            types = jsonHelper.getTypes();
            foreach(Type type in types)
            {
                typesDict.Add(type.ID, type);
            }
            rarities = jsonHelper.getRarities();
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
            if(id == -1)
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
        /// Sends searches for items with the given name, filters the results
        /// according to search options, and store the results in searchResults
        /// </summary>
        /// <param name="itemName">The item name being searched</param>
        public void search(string itemName)
        {
            searchResults.Clear();
            List<Item> results = jsonHelper.searchItem(itemName);
            if(results == null)
            {
                //TODO: Message Dialog to user
            }
            var linqResults = from item in results
                              where item.RestrictionLevel >= minLvl &&
                                    item.RestrictionLevel <= maxLvl
                              select item;

            if(currentType != null)
            {
                linqResults = linqResults.Where(item => item.TypeId == currentType.ID);
            }

            if(currentSubtype != null)
            {
                linqResults = linqResults.Where(item => item.SubtypeId == currentSubtype.ID);
            }

            if(currentRarity != null)
            {
                linqResults = linqResults.Where(item => item.RarityId == currentRarity.ID);
            }

            searchResults = linqResults.ToList();

        }

        /// <summary>
        /// Gets a list of DisplayItems from the searchResults list
        /// </summary>
        /// <returns>List of DisplayItem objects that match current search</returns>
        public List<DisplayItem> getDisplayItems()
        {
            List<DisplayItem> displayItems = new List<DisplayItem>();
            foreach(Item item in searchResults)
            {
                Type itemType = typesDict[item.TypeId];
                string typeString = "";
                if(itemType.Subtypes.Count < 1)
                {
                    typeString = itemType.Name;
                }
                else
                {
                    typeString = itemType.Name + " // " + itemType.Subtypes[item.SubtypeId].Name;
                }
                displayItems.Add(new DisplayItem(item));
            }
            return displayItems;
        }
    }
}
