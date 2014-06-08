using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlackLionTrader
{
    class SearchModel
    {
        private List<string> types = new List<string>();

        public List<string> Types
        {
            get { return types; }
        }

        private List<string> rarities = new List<string>();

        public List<string> Rarities
        {
            get { return rarities; }
        }
        public SearchModel()
        {
        }

    }
}
