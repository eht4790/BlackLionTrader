/// JsonItemDefinitions.cs
///
/// Contains all the class definitions for objects from
/// the Json results

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackLionTrader
{

    public class Subtype
    {
        private int id;

        public int ID
        {
            get { return id; }
        }

        private string name;

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

    public class Type
    {
        private int id;

        public int ID
        {
            get { return id; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private List<Subtype> subtypes;

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

    public class Rarity
    {
        private int id;

        public int ID
        {
            get { return id; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        public Rarity(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class Item
    {
        private int data_id;

        public int DataId
        {
            get { return data_id; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private int rarity;

        public int RarityId
        {
            get { return rarity; }
        }

        private int restriction_level;

        public int RestrictionLevel
        {
            get { return restriction_level; }
        }

        private string img;

        public string Img
        {
            get { return img; }
        }

        private int type_id;

        public int TypeId
        {
            get { return type_id; }
        }

        private int sub_type_id;

        public int SubTypeId
        {
            get { return sub_type_id; }
        }

        private string price_last_changed;

        public string PriceLastChanged
        {
            get { return price_last_changed; }
        }

        private int max_offer_unit_price;

        public int MaxOffer
        {
            get { return max_offer_unit_price; }
        }

        private int min_sale_unit_price;

        public int MinSale
        {
            get { return min_sale_unit_price; }
        }

        private int offer_availability;

        public int OfferAvailability
        {
            get { return offer_availability; }
        }

        private int sale_availability;

        public int SaleAvailability
        {
            get { return sale_availability; }
        }

        private int sale_price_change_last_hour;

        public int SalePriceChange
        {
            get { return sale_price_change_last_hour; }
        }

        private int offer_price_change_last_hour;

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

    public class GemPrice
    {
        private int gem_to_gold;

        public int GemToGold
        {
            get { return gem_to_gold; }
        }

        private int gold_to_gem;

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
