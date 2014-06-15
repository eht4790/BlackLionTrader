
/* 
 * MainPage.xmal.cs
 *
 * The code behind for MainPage.xaml. Contains collections for binding
 * and even handlers for MainPage UI controls
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackLionTrader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ObservableCollection<string> types = new ObservableCollection<string>();
        private ObservableCollection<string> subtypes = new ObservableCollection<string>();
        private ObservableCollection<string> rarities = new ObservableCollection<string>();
        private App app = Application.Current as App;

        /// <summary>
        /// A collection of strings with all the various item types
        /// </summary>
        public ObservableCollection<string> Types
        {
            get { return types; }
        }

        /// <summary>
        /// A collection of strings with all the subtypes of the currently selected Type
        /// </summary>
        public ObservableCollection<string> SubTypes
        {
            get { return subtypes; }
        }

        /// <summary>
        /// A collection of strings with all the rarity levels of an item
        /// </summary>
        public ObservableCollection<string> Rarities
        {
            get { return rarities;  }
        }

        public MainPage()
        {
            this.InitializeComponent();

            // Adjust width of hub sections according to screen resolution
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;
            SearchSection.Width = (Int32)(width * .8);
            WatchSection.Width = (Int32)(width * .8);
            types = new ObservableCollection<string>(app.controller.getTypesAsString());
            rarities = new ObservableCollection<string>(app.controller.getRaritiesAsString());
        }

        /// <summary>
        /// Event handler for focusing on the searchBox. Removes any default
        /// text that was in the box.
        /// </summary>
        /// <param name="sender">The searchBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void ItemSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if(searchBox.Text.Equals("Item Name"))
            {
                searchBox.Text = string.Empty;
                searchBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        /// <summary>
        /// Event handler for removing focus from the searchBox. Restores default
        /// text if empty.
        /// </summary>
        /// <param name="sender">The searchBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void ItemSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (searchBox.Text.Equals(""))
            {
                searchBox.Text = "Item Name";
                searchBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Event handler for focusing on either the MinLvl or MaxLvl TextBoxes.
        /// Removes any default text that might be there.
        /// </summary>
        /// <param name="sender">The MinLvl or MaxLvl TextBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void LevelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if(lvlBox.Text.Equals("Min Lvl") || lvlBox.Text.Equals("Max Lvl"))
            {
                lvlBox.Text = string.Empty;
                lvlBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        /// <summary>
        /// Event handler for losing focus on the MinLvl Textbox. Restores default
        /// text if left empty.
        /// </summary>
        /// <param name="sender">The MinLvl Textbox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void MinLevelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if(lvlBox.Text.Equals(""))
            {
                lvlBox.Text = "Min Lvl";
                lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Event handler for losing focus on the MaxLvl Textbox. Restores default
        /// text if left empty.
        /// </summary>
        /// <param name="sender">The MaxLvl Textbox in the Search hub section.</param>
        /// <param name="e">Event data</param>
        private void MaxLevelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if (lvlBox.Text.Equals(""))
            {
                lvlBox.Text = "Max Lvl";
                lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Event handler for when a type is selected from TypeCB. Populates the
        /// SubtypeCB from the possible Subtypes of the selected Type.
        /// </summary>
        /// <param name="sender">The TypeCB Combobox in the Search hub section.</param>
        /// <param name="e">Event data</param>
        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox typeBox = (ComboBox)sender;
            app.controller.setType(typeBox.SelectedIndex);
            List<string> list = app.controller.getSubtypesAsString();
            subtypes.Clear();
            foreach(string subtype in list)
            {
                subtypes.Add(subtype);
            }
        }
    }
}
