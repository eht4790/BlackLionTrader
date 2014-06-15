﻿/*
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
            jsonHelper = new JsonHelper();
            searchModel = new SearchModel(jsonHelper);
            watchModel = new WatchModel(jsonHelper);
            gemModel = new GemsModel(jsonHelper);
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
        public List<string> getRaritiesAsString()
        {
            List<string> rarities = new List<string>();
            foreach(Rarity rarity in searchModel.Rarities)
            {
                rarities.Add(rarity.Name);
            }
            return rarities;
        }

        /// <summary>
        /// Change the current Type in the searchModel;
        /// </summary>
        /// <param name="id">The id of the selected Type</param>
        public void setType(int id)
        {
            searchModel.changeType(id);
        }
    }
}
