/// SearchModel.cs
///
/// Tracks the state of the Search hub section

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlackLionTrader
{
    class SearchModel
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
