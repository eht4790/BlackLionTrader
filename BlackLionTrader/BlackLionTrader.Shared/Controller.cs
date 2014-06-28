/*
 * Controller.cs
 *
 * Handles calls from the View and makes the appropriate
 * calls the corresponding Model.
 *
 *  Copyright (C) 2014  Eric Trumble https://github.com/eht4790
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
    public class Controller
    {
        private SearchModel searchModel;
        private WatchModel watchModel;
        private GemsModel gemModel;
        private JsonHelper jsonHelper;

        public Controller()
        {
            try
            {
                jsonHelper = new JsonHelper();
                searchModel = new SearchModel(jsonHelper);
                watchModel = new WatchModel(jsonHelper);
                gemModel = new GemsModel(jsonHelper);
            }
            catch(AggregateException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets a list of Type names for the UI
        /// </summary>
        /// <returns>A List of strings of all Type names</returns>
        public List<string> getTypesAsString()
        {
            List<string> types = new List<string>();
            foreach(Type type in searchModel.Types)
            {
                types.Add(type.Name);
            }
            return types;
        }

        /// <summary>
        /// Gets a list of Subtype names for the UI
        /// </summary>
        /// <returns>A List of strings of all Subtype names</returns>
        public List<string> getSubtypesAsString()
        {
            List<string> subtypes = new List<string>();
            foreach(Subtype subtype in searchModel.SubTypes)
            {
                subtypes.Add(subtype.Name);
            }
            return subtypes;
        }

        /// <summary>
        /// Gets a list of Rarity names for the UI
        /// </summary>
        /// <returns>A List of strings of all Rarity names</returns>
        public List<Rarity> getRarities()
        {
            List<Rarity> rarities = new List<Rarity>();
            foreach(Rarity rarity in searchModel.Rarities)
            {
                rarities.Add(rarity);
            }
            return rarities;
        }

        /// <summary>
        /// Change the current Type in the searchModel
        /// </summary>
        /// <param name="id">The id of the selected Type</param>
        public void setType(int id)
        {
            searchModel.changeType(id);
        }

        /// <summary>
        /// Change the current Subtype in the searchModel
        /// </summary>
        /// <param name="id">The id of the selected Subtype</param>
        public void setSubtype(int id)
        {
            searchModel.changeSubtype(id);
        }

        /// <summary>
        /// Change the current Rarity in the searchModel
        /// </summary>
        /// <param name="id">The id of the selected Rarity</param>
        public void setRarity(int id)
        {
            searchModel.changeRarity(id);
        }

        /// <summary>
        /// Change the current Minimum Level in the searchModel
        /// </summary>
        /// <param name="lvl">The new minimum level</param>
        public void setMinLvl(int lvl)
        {
            searchModel.changeMinLvl(lvl);
        }

        /// <summary>
        /// Gets the MinLvl stored in the SearchModel
        /// </summary>
        /// <returns>The minimum level for an item in search results</returns>
        public int getMinLvl()
        {
            return searchModel.MinLvl;
        }

        /// <summary>
        /// Gets the MaxLvl stored int he SearchModel
        /// </summary>
        /// <returns>The maximum level for an item in search results</returns>
        public int getMaxLvl()
        {
            return searchModel.MaxLvl;
        }

        /// <summary>
        /// Change the current Maximum Level in the searchModel
        /// </summary>
        /// <param name="lvl">The new maximum level</param>
        public void setMaxLvl(int lvl)
        {
            searchModel.changeMaxLvl(lvl);
        }

        public List<DisplayItem> searchItems(string itemName)
        {
            try
            {
                searchModel.search(itemName);
                return searchModel.getDisplayItems();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
