/*
 * JsonItemDefinitions.cs
 * 
 * Contains all the class definitions for object from the
 * Json results.
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

    /// <summary>
    /// Represents a subtype of an existing item type.
    /// Ex. Type-Armor SubType-Head, Shoulders, Chest, etc...
    /// </summary>
    public class Subtype
    {
        private int id;
        private string name;

        /// <summary>
        /// The unique id of the subtype
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// The name of the subtype
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        public Subtype(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    /// <summary>
    /// A category of item
    /// Ex. Armor, Crafting Material, Consumable, etc...
    /// </summary>
    public class Type
    {
        private int id;
        private string name;
        private List<Subtype> subtypes;

        /// <summary>
        /// The unique idea of the type
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// The name of the type
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The list of subtypes that belong to this type
        /// </summary>
        public List<Subtype> Subtypes
        {
            get { return subtypes; }
        }

        public Type(int id, string name, List<Subtype> subtypes)
        {
            this.id = id;
            this.name = name;
            this.subtypes = subtypes;
        }
    }

    /// <summary>
    /// The rarity level of the item
    /// Ex. Basic, Fine, Masterwork, etc...
    /// </summary>
    public class Rarity
    {
        private int id;
        private string name;
        private string color;

        /// <summary>
        /// The unique id of the rarity level
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// The name of the rarity level
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The color of the font used for the rarity
        /// </summary>
        public string Color
        {
            get { return color; }
        }

        public Rarity(int id, string name)
        {
            this.id = id;
            this.name = name;
            switch (id)
            {
                case 0:
                    this.color = "Black";
                    break;

                case 1:
                    this.color = "Black";
                    break;

                case 2:
                    this.color = "DodgerBlue";
                    break;

                case 3:
                    this.color = "LimeGreen";
                    break;

                case 4:
                    this.color = "Goldenrod";
                    break;

                case 5:
                    this.color = "DarkOrange";
                    break;

                case 6:
                    this.color = "DeepPink";
                    break;

                case 7:
                    this.color = "Purple";
                    break;

                default:
                    this.color = "Black";
                    break;
            }
        }
    }

    /// <summary>
    /// A specific item in the game
    /// Ex. Iron ore, Apothecary's Barbaric Helm, etc...
    /// </summary>
    public class Item
    {
        private int data_id;
        private string name;
        private int rarity;
        private int restriction_level;
        private string img;
        private int type_id;
        private int sub_type_id;
        private string price_last_changed;
        private int max_offer_unit_price;
        private int min_sale_unit_price;
        private int offer_availability;
        private int sale_availability;
        private int sale_price_change_last_hour;
        private int offer_price_change_last_hour;

        /// <summary>
        /// The unique id of the item
        /// </summary>
        public int DataId
        {
            get { return data_id; }
        }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The id of the item's rarity level
        /// </summary>
        public int RarityId
        {
            get { return rarity; }
        }

        /// <summary>
        /// The minimum character level required to use this item
        /// </summary>
        public int RestrictionLevel
        {
            get { return restriction_level; }
        }

        /// <summary>
        /// A url for an img of the item
        /// </summary>
        public string Img
        {
            get { return img; }
        }

        /// <summary>
        /// The id of the item's type
        /// </summary>
        public int TypeId
        {
            get { return type_id; }
        }

        /// <summary>
        /// The id of the item's subtype
        /// </summary>
        public int SubtypeId
        {
            get { return sub_type_id; }
        }

        /// <summary>
        /// When the price of the item was last changed.
        /// "YYYY-MM-DD HH:II:SS UTC"
        /// </summary>
        public string PriceLastChanged
        {
            get { return price_last_changed; }
        }

        /// <summary>
        /// The current highest offer listed for the item
        /// </summary>
        public int MaxOffer
        {
            get { return max_offer_unit_price; }
        }

        /// <summary>
        /// The current lowest sell price for the item
        /// </summary>
        public int MinSale
        {
            get { return min_sale_unit_price; }
        }

        /// <summary>
        /// The number of buy orders for the item
        /// </summary>
        public int OfferAvailability
        {
            get { return offer_availability; }
        }

        /// <summary>
        /// The number of sell listings for the item
        /// </summary>
        public int SaleAvailability
        {
            get { return sale_availability; }
        }

        /// <summary>
        /// The percentage of change in the sell price since the last hour
        /// </summary>
        public int SalePriceChange
        {
            get { return sale_price_change_last_hour; }
        }

        /// <summary>
        /// The percentage of change in the offer price since the last hour
        /// </summary>
        public int OfferPriceChange
        {
            get { return offer_price_change_last_hour; }
        }

        public Item(int data_id, string name, int rarity, int restriction_level, string img, int type_id, int sub_type_id, string price_last_changed,
                    int max_offer_unit_price, int min_sale_unit_price, int offer_availability, int sale_availability, int sale_price_change_last_hour, int offer_price_change_last_hour)
        {
            this.data_id = data_id;
            this.name = name;
            this.rarity = rarity;
            this.restriction_level = restriction_level;
            this.img = img;
            this.type_id = type_id;
            this.sub_type_id = sub_type_id;
            this.price_last_changed = price_last_changed;
            this.max_offer_unit_price = max_offer_unit_price;
            this.min_sale_unit_price = min_sale_unit_price;
            this.offer_availability = offer_availability;
            this.sale_availability = sale_availability;
            this.sale_price_change_last_hour = sale_price_change_last_hour;
            this.offer_price_change_last_hour = offer_price_change_last_hour;
        }
    }

    /// <summary>
    /// Represents a sell/buy listing at a particular price currently on the Trading Post
    /// </summary>
    public class ItemListing
    {
        string listing_datetime;
        int unit_price;
        int quantity;
        int listings;

        /// <summary>
        /// The date the listing was put on the Trading Post
        /// </summary>
        public string ListingDate
        {
            get { return listing_datetime; }
        }

        /// <summary>
        /// The price of one item the listing is for
        /// </summary>
        public int UnitPrice
        {
            get { return unit_price; }
        }

        /// <summary>
        /// The amount of the item requested/posted 
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
        }

        /// <summary>
        /// The amount of listings at the same price
        /// </summary>
        public int Listings
        {
            get { return listings; }
        }

        public ItemListing(string listingDate, int unitPrice, int quantity, int listings)
        {
            this.listing_datetime = listingDate;
            this.unit_price = UnitPrice;
            this.quantity = quantity;
            this.listings = listings;
        }
    }

    /// <summary>
    /// Contains the current conversion prices for gems and gold
    /// </summary>
    public class GemPrice
    {
        private int gem_to_gold;
        private int gold_to_gem;

        /// <summary>
        /// The amount of gold 100 gems is worth
        /// </summary>
        public int GemToGold
        {
            get { return gem_to_gold; }
        }

        /// <summary>
        /// The amount of gems 100 gold is worth
        /// </summary>
        public int GoldToGem
        {
            get { return gold_to_gem; }
        }

        public GemPrice(int gemToGold, int goldToGem)
        {
            this.gem_to_gold = gemToGold;
            this.gold_to_gem = goldToGem;
        }
    }

}
