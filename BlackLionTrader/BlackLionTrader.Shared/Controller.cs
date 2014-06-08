/// Controller.cs
/// 
/// Handles calls from the View and makes the appropriate
/// calls the corresponding Model.

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackLionTrader
{
    class Controller
    {
        private SearchModel searchModel = new SearchModel();
        private WatchModel watchModel = new WatchModel();
        private GemsModel gemModel = new GemsModel();

        /// <summary>
        /// A reference to the SearchModel the stores the state
        /// for the Search hub section.
        /// </summary>
        public SearchModel SearchModel
        {
            get { return searchModel; }
        }

        /// <summary>
        /// A reference to the WatchModel that stores the state
        /// for the Watch hub section.
        /// </summary>
        public WatchModel WatchModel
        {
            get { return watchModel; }
        }

        /// <summary>
        /// A reference to the GemModel that stores the state
        /// for the Gems hub section.
        /// </summary>
        public GemsModel GemModel
        {
            get { return gemModel; }
        }

        public Controller()
        {
        }
    }
}
