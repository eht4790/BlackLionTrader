/*
 * Controller.cs
 *
 * Handles calls from the View and makes the appropriate
 * calls the corresponding Model.
 *
 *  Copyright (C) 2014  Eric Trumble https://github.com/eht4790
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
    public class Controller
    {
        private SearchModel searchModel = new SearchModel();
        private WatchModel watchModel = new WatchModel();
        private GemsModel gemModel = new GemsModel();
        private JsonHelper jsonHelper = new JsonHelper();

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
