/* 
 * WatchModel.cs
 * 
 * Tracks the state of the Watch hub section
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

namespace BlackLionTrader
{
    public class WatchModel
    {
        private JsonHelper jsonHelper;
        private CommonResources resources;

        private List<Subtype> subtypes;
        
        private Type currentType;
        private Subtype currentSubtype;
        private Rarity currentRarity;

        private int minLvl = 0;
        private int maxLvl = 80;

        private Dictionary<int, Item> favorites = new Dictionary<int, Item>();

        /// <summary>
        /// The currently selected Type to filter favorites by
        /// </summary>
        public Type CurrentType
        {
            get { return currentType; }
        }

        /// <summary>
        /// The currently selected Subtype to filter favorites by
        /// </summary>
        public Subtype CurrentSubtype
        {
            get { return CurrentSubtype; }
        }

        /// <summary>
        /// The currently selected Rarity to filter favorites by
        /// </summary>
        public Rarity CurrentRarity
        {
            get { return currentRarity; }
        }

        /// <summary>
        /// The current minimum level of an item to filter favorites by
        /// </summary>
        public int MinLvl
        {
            get { return minLvl; }
        }

        /// <summary>
        /// The current maximum level of an item to filter favorites by
        /// </summary>
        public int MaxLvl
        {
            get { return maxLvl; }
        }

        public WatchModel(JsonHelper jsonHelper, CommonResources resources)
        {
            this.jsonHelper = jsonHelper;
            this.resources = resources;
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
                currentType = resources.getType(id);
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
            if (id == 0)
            {
                currentRarity = null;
            }
            else
            {
                currentRarity = resources.getRarity(id);
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
            if (lvl > 0)
            {
                if (lvl <= maxLvl)
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
            if (lvl <= 80)
            {
                if (lvl >= minLvl)
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
        /// Adds the given item to the favorites Dictionary
        /// </summary>
        /// <param name="item">The Item object to be added</param>
        public void addItem(Item item)
        {
            favorites.Add(item.ID, item);
        }

        /// <summary>
        /// Removes the value of the given item ID from the
        /// the favorites Dictionary
        /// </summary>
        /// <param name="id">The id of the Item to be removed</param>
        public void removeItem(int id)
        {
            favorites.Remove(id);
        }

        /// <summary>
        /// Returns whether or not the item of the given id exists in the
        /// the favorites Dicitonary
        /// </summary>
        /// <param name="id">The id of the item to check</param>
        /// <returns>True if the item exists, false otherwise</returns>
        public bool contains(int id)
        {
            return favorites.ContainsKey(id);
        }

        /// <summary>
        /// Returns the corresponding Item of the given id
        /// </summary>
        /// <param name="id">The id of the desired item</param>
        /// <returns>The corresponding Item object</returns>
        public Item getItem(int id)
        {
            return favorites[id];
        }

        /// <summary>
        /// Returns the corresponding DisplayItem of the given id
        /// </summary>
        /// <param name="id">The id fo the desered item</param>
        /// <returns>The a DisplayItem object of the given id</returns>
        public DisplayItem getDisplayItem(int id)
        {
            return new DisplayItem(favorites[id]);
        }

        /// <summary>
        /// Rerturns a list of Values in the favorites Dictionary as DisplayItems
        /// </summary>
        /// <returns>A list of all the items that were favorited as DisplayItems</returns>
        public List<DisplayItem> getFavorites()
        {
            List<DisplayItem> displayItems = new List<DisplayItem>();
            foreach(Item item in favorites.Values)
            {
                displayItems.Add(new DisplayItem(item));
            }
            return displayItems;
        }
    }
}
