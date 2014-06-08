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
        private List<string> types = new List<string>();
        private List<string> rarities = new List<string>();

        /// <summary>
        /// A list of available types to filter search results
        /// </summary>
        public List<string> Types
        {
            get { return types; }
        }

        /// <summary>
        /// A list of available rarity levels to filter search results
        /// </summary>
        public List<string> Rarities
        {
            get { return rarities; }
        }

        public SearchModel()
        {
        }

    }
}
