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
        private Dictionary<int, Item> favorites = new Dictionary<int, Item>();

        public WatchModel(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
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
