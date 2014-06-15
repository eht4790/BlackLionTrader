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


namespace BlackLionTrader
{
    public class SearchModel
    {
        private JsonHelper jsonHelper;
        private List<Type> types = new List<Type>();
        private List<Subtype> subtypes = new List<Subtype>();
        private List<Rarity> rarities = new List<Rarity>();

        private Type currentType = null;
        private Subtype currentSubtype = null;
        private Rarity currentRarity = null;


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
    }
}
