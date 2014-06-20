/*
 * DisplayItem.cs
 * 
 * Contains the information of an item to be displayed in the UI ListBox
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
    public class DisplayItem
    {
        private string img;
        private string name;
        private int level;
        private string rarity;
        private string type;
        private int supply;
        private int demand;
        private int minSaleOffer;
        private int maxBuyOffer;
        private int margin;

        /// <summary>
        /// The url of the image associate with the file
        /// </summary>
        public string Img
        {
            get { return img; }
        }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The restriction level of the item
        /// </summary>
        public int Level
        {
            get { return level; }
        }

        /// <summary>
        /// The name of the rarity of the item
        /// </summary>
        public string Rarity
        {
            get { return rarity; }
        }

        /// <summary>
        /// The name of the type and subtype if applicable
        /// Ex. Weapon // Sword
        /// </summary>
        public string Type
        {
            get { return type; }
        }

        /// <summary>
        /// The quantity of sell offers on the market
        /// </summary>
        public int Supply
        {
            get { return supply; }
        }

        /// <summary>
        /// The quantity of buy offers on the market
        /// </summary>
        public int Demand
        {
            get { return demand; }
        }

        /// <summary>
        /// The lowest sell offer on the market
        /// </summary>
        public int MinSaleOffer
        {
            get { return minSaleOffer; }
        }

        /// <summary>
        /// The highest buy offer on the market
        /// </summary>
        public int MaxBuyOffer
        {
            get { return maxBuyOffer; }
        }

        /// <summary>
        /// The difference between the minSaleOffer and maxBuyOffer with
        /// the listing fee and selling fee included (15% tax)
        /// </summary>
        public int Margin
        {
            get { return margin; }
        }

        public DisplayItem(string img,
                           string name,
                           int level,
                           string rarity,
                           string type,
                           int supply,
                           int demand,
                           int minSaleOffer,
                           int maxBuyOffer)
        {
            this.img = img;
            this.name = name;
            this.level = level;
            this.rarity = rarity;
            this.type = type;
            this.supply = supply;
            this.demand = demand;
            this.minSaleOffer = minSaleOffer;
            this.maxBuyOffer = maxBuyOffer;
            this.margin = (int)(minSaleOffer * .85) - maxBuyOffer;
        }

    }
}
