/*
 * CommonResources.cs
 * 
 * Stores common data used by the multiple models
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
    public class CommonResources
    {

        private JsonHelper jsonHelper;
        private List<Type> types = new List<Type>();
        private List<Rarity> rarities = new List<Rarity>();

        /// <summary>
        /// A list of possible Type objects
        /// </summary>
        public List<Type> Types
        {
            get { return types; }
        }

        /// <summary>
        /// A list of possible Rarity objects
        /// </summary>
        public List<Rarity> Rarities
        {
            get { return rarities; }
        }

        public CommonResources(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
            try
            {
                types = jsonHelper.getTypes();
                rarities = jsonHelper.getRarities();
                rarities.Insert(0, new Rarity(-1, "Any"));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns the Type at the given index
        /// </summary>
        /// <param name="index">The index of the desired Type in the list</param>
        /// <returns>The requested Type</returns>
        public Type getType(int index)
        {
            return types[index];
        }

        /// <summary>
        /// Returns the Rarity at the given index
        /// </summary>
        /// <param name="index">The index of the desired Rarity in the list</param>
        /// <returns>The requested Rarity</returns>
        public Rarity getRarity(int index)
        {
            return rarities[index];
        }
    }
}
