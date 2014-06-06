using System;
using System.Collections.Generic;
using System.Text;

namespace BlackLionTrader
{
    class Controller
    {
        private SearchModel searchModel = new SearchModel();

        public SearchModel SearchModel
        {
            get { return searchModel; }
        }

        private WatchModel watchModel = new WatchModel();

        public WatchModel WatchModel
        {
            get { return watchModel; }
        }

        private GemsModel gemModel = new GemsModel();

        public GemsModel GemModel
        {
            get { return gemModel; }
        }

        public Controller()
        {

        }
    }
}
