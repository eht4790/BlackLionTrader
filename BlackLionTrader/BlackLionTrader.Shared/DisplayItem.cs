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
        private int id;
        private string img;
        private string name;
        private int level;
        private string rarityColor;
        private int supply;
        private int demand;
        private string minSaleOffer;
        private string minSaleCopper;
        private string minSaleCopperImg = null;
        private string minSaleSilver;
        private string minSaleSilverImg = null;
        private string minSaleGold;
        private string minSaleGoldImg = null;
        private string maxBuyOffer;
        private string maxBuyCopper;
        private string maxBuyCopperImg = null;
        private string maxBuySilver;
        private string maxBuySilverImg = null;
        private string maxBuyGold;
        private string maxBuyGoldImg = null;
        private string margin;
        private int marginInt;
        private string marginCopper;
        private string marginCopperImg = null;
        private string marginSilver;
        private string marginSilverImg = null;
        private string marginGold;
        private string marginGoldImg = null;
        private bool watched = false;

        /// <summary>
        /// The id of the item
        /// </summary>
        public int ID
        {
            get { return id; }
        }

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
        public string RarityColor
        {
            get { return rarityColor; }
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
        public string MinSaleOffer
        {
            get { return minSaleOffer; }
        }

        /// <summary>
        /// The copper value of the lowest sell offer
        /// </summary>
        public string MinSaleCopper
        {
            get { return minSaleCopper; }
        }

        /// <summary>
        /// The source for copper coin image used in displaying the min sale price.
        /// Null if image is uneeded.
        /// </summary>
        public string MinSaleCopperImg
        {
            get { return minSaleCopperImg; }
        }

        /// <summary>
        /// The silver value of the lowest sell offer
        /// </summary>
        public string MinSaleSilver
        {
            get { return minSaleSilver; }
        }

        /// <summary>
        /// The source for silver coin image used in displaying the min sale price.
        /// Null if image is uneeded.
        /// </summary>
        public string MinSaleSilverImg
        {
            get { return minSaleSilverImg; }
        }

        /// <summary>
        /// The gold value of the lowest sell offer
        /// </summary>
        public string MinSaleGold
        {
            get { return minSaleGold; }
        }

        /// <summary>
        /// The source for gold coin image used in displaying the min sale price.
        /// Null if image is uneeded.
        /// </summary>
        public string MinSaleGoldImg
        {
            get { return minSaleGoldImg; }
        }

        /// <summary>
        /// The highest buy order on the market
        /// </summary>
        public string MaxBuyOffer
        {
            get { return maxBuyOffer; }
        }

        /// <summary>
        /// The copper value of the highest buy order
        /// </summary>
        public string MaxBuyCopper
        {
            get { return maxBuyCopper; }
        }

        /// <summary>
        /// The source for copper coin image used in displaying the max buy price.
        /// Null if image is uneeded.
        /// </summary>
        public string MaxBuyCopperImg
        {
            get { return maxBuyCopperImg; }
        }

        /// <summary>
        /// The silver value of the highest buy order
        /// </summary>
        public string MaxBuySilver
        {
            get { return maxBuySilver; }
        }

        /// <summary>
        /// The source for silver coin image used in displaying the max buy price.
        /// Null if image is uneeded.
        /// </summary>
        public string MaxBuySilverImg
        {
            get { return maxBuySilverImg;}
        }

        /// <summary>
        /// The gold value of the highest buy order
        /// </summary>
        public string MaxBuyGold
        {
            get { return maxBuyGold; }
        }

        /// <summary>
        /// The source for gold coin image used in displaying the max buy price.
        /// Null if image is uneeded.
        /// </summary>
        public string MaxBuyGoldImg
        {
            get { return maxBuyGoldImg; }
        }

        /// <summary>
        /// The difference between the minSaleOffer and maxBuyOffer with
        /// the listing fee and selling fee included (15% tax)
        /// </summary>
        public string Margin
        {
            get { return margin; }
        }

        /// <summary>
        /// The copper value of the difference between the minSaleOffer with
        /// the listing fee and selling fee included (15% tax)
        /// </summary>
        public string MarginCopper
        {
            get { return marginCopper; }
        }

        /// <summary>
        /// The source for copper coin image used in displaying the margin.
        /// Null if image is uneeded.
        /// </summary>
        public string MarginCopperImg
        {
            get { return marginCopperImg; }
        }

        /// <summary>
        /// The silver value of the difference between the minSaleOffer with
        /// the listing fee and selling fee included (15% tax)
        /// </summary>
        public string MarginSilver
        {
            get { return marginSilver; }
        }

        /// <summary>
        /// The source for silver coin image used in displaying the margin.
        /// Null if image is uneeded.
        /// </summary>
        public string MarginSilverImg
        {
            get { return marginSilverImg; }
        }

        /// <summary>
        /// The gold value of the difference between the minSaleOffer with
        /// the listing fee and selling fee included (15% tax)
        /// </summary>
        public string MarginGold
        {
            get { return marginGold; }
        }

        /// <summary>
        /// The source for gold coin image used in displaying the margin.
        /// Null if image is uneeded.
        /// </summary>
        public string MarginGoldImg
        {
            get { return marginGoldImg; }
        }

        /// <summary>
        /// The source for the Watch/Unwatch icon.
        /// </summary>
        public string WatchImg
        {
            get 
            {
                if(watched)
                {
                    return "SharedAssets/watch.png";
                }
                else
                {
                    return "SharedAssets/unwatch.png";
                }
            }
        }

        public DisplayItem(Item item)
        {
            this.img = item.Img;
            if(item.Name.Length > 34)
            {
                this.name = item.Name.Substring(0, 31) + "...";
            }
            else
            {
                this.name = item.Name.PadRight(34);
            }
            this.name = item.Name;
            this.level = item.RestrictionLevel;
            switch (item.RarityId)
            {
                case 0:
                    rarityColor = "Black";
                    break;

                case 1:
                    rarityColor = "Black";
                    break;

                case 2:
                    rarityColor = "DodgerBlue";
                    break;

                case 3:
                    rarityColor = "LimeGreen";
                    break;

                case 4:
                    rarityColor = "Goldenrod";
                    break;

                case 5:
                    rarityColor = "DarkOrange";
                    break;

                case 6:
                    rarityColor = "DeepPink";
                    break;

                case 7:
                    rarityColor = "Purple";
                    break;

                default:
                    rarityColor = "Black";
                    break;
            }
            this.supply = item.SaleAvailability;
            this.demand = item.OfferAvailability;
            this.minSaleOffer = item.MinSale.ToString();

            // Divide up minSaleOffer price
            if(this.minSaleOffer.Length < 3)
            {
                minSaleCopper = this.minSaleOffer;
                minSaleSilver = "";
                minSaleGold = "";
                minSaleCopperImg = "SharedAssets/copper.png";
            }
            else if(this.minSaleOffer.Length < 4)
            {
                minSaleCopper = this.minSaleOffer.Substring(1);
                minSaleSilver = " " + this.minSaleOffer.Substring(0,1);
                minSaleGold = "";
                minSaleCopperImg = "SharedAssets/copper.png";
                minSaleSilverImg = "SharedAssets/silver.png";
            }
            else if(this.minSaleOffer.Length < 5)
            {
                minSaleCopper = this.minSaleOffer.Substring(2);
                minSaleSilver = this.minSaleOffer.Substring(0, 2);
                minSaleGold = "";
                minSaleCopperImg = "SharedAssets/copper.png";
                minSaleSilverImg = "SharedAssets/silver.png";
            }
            else if(this.minSaleOffer.Length >= 5)
            {
                int endGoldIndex = this.minSaleOffer.Length - 4;
                minSaleCopper = this.minSaleOffer.Substring(endGoldIndex + 2);
                minSaleSilver = this.minSaleOffer.Substring(endGoldIndex, 2);
                minSaleGold = this.minSaleOffer.Substring(0, endGoldIndex);
                minSaleCopperImg = "SharedAssets/copper.png";
                minSaleSilverImg = "SharedAssets/silver.png";
                minSaleGoldImg = "SharedAssets/gold.png";
            }

            // Divide up maxBuyOffer price
            this.maxBuyOffer = item.MaxOffer.ToString();
            if (this.maxBuyOffer.Length < 3)
            {
                maxBuyCopper = this.maxBuyOffer;
                maxBuySilver = "";
                maxBuyGold = "";
                maxBuyCopperImg = "SharedAssets/copper.png";
            }
            else if (this.maxBuyOffer.Length < 4)
            {
                maxBuyCopper = this.maxBuyOffer.Substring(1);
                maxBuySilver = " " + this.maxBuyOffer.Substring(0, 1);
                maxBuyGold = "";
                maxBuyCopperImg = "SharedAssets/copper.png";
                maxBuySilverImg = "SharedAssets/silver.png";
            }
            else if (this.maxBuyOffer.Length < 5)
            {
                maxBuyCopper = this.maxBuyOffer.Substring(2);
                maxBuySilver = this.maxBuyOffer.Substring(0, 2);
                maxBuyGold = "";
                maxBuyCopperImg = "SharedAssets/copper.png";
                maxBuySilverImg = "SharedAssets/silver.png";
            }
            else if (this.maxBuyOffer.Length >= 5)
            {
                int endGoldIndex = this.maxBuyOffer.Length - 4;
                maxBuyCopper = this.maxBuyOffer.Substring(endGoldIndex + 2);
                maxBuySilver = this.maxBuyOffer.Substring(endGoldIndex, 2);
                maxBuyGold = this.maxBuyOffer.Substring(0, endGoldIndex);
                maxBuyCopperImg = "SharedAssets/copper.png";
                maxBuySilverImg = "SharedAssets/silver.png";
                maxBuyGoldImg = "SharedAssets/gold.png";
            }

            // Divide up margin price
            this.marginInt = (int)(item.MinSale * .85) - item.MaxOffer;
            if (marginInt < 0)
            {
                this.margin = marginInt.ToString().Substring(1);
            }
            else
            {
                this.margin = marginInt.ToString();
            }
            if (this.margin.Length < 3)
            {
                marginCopper = this.margin;
                marginSilver = "";
                marginGold = "";
                if(marginInt < 0)
                {
                    marginCopper = "-" + marginCopper;
                }
                marginCopperImg = "SharedAssets/copper.png";
            }
            else if (this.margin.Length < 4)
            {
                marginCopper = this.margin.Substring(1);
                marginSilver = " " + this.margin.Substring(0, 1);
                marginGold = "";
                if(marginInt < 0)
                {
                    marginSilver = "-" + marginSilver;
                }
                marginCopperImg = "SharedAssets/copper.png";
                marginSilverImg = "SharedAssets/silver.png";
            }
            else if (this.margin.Length < 5)
            {
                marginCopper = this.margin.Substring(2);
                marginSilver = this.margin.Substring(0, 2);
                marginGold = "";
                if(marginInt < 0)
                {
                    marginSilver = "-" + marginSilver;
                }
                marginCopperImg = "SharedAssets/copper.png";
                marginSilverImg = "SharedAssets/silver.png";
            }
            else if (this.margin.Length >= 5)
            {
                int endGoldIndex = this.margin.Length - 4;
                marginCopper = this.margin.Substring(endGoldIndex + 2);
                marginSilver = this.margin.Substring(endGoldIndex, 2);
                marginGold = this.margin.Substring(0, endGoldIndex);
                if(marginInt < 0)
                {
                    marginGold = "-" + marginGold;
                }
                marginCopperImg = "SharedAssets/copper.png";
                marginSilverImg = "SharedAssets/silver.png";
                marginGoldImg = "SharedAssets/gold.png";
            }
        }

    }
}
