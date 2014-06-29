
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
using Windows.UI.Popups;

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
        private ObservableCollection<Rarity> rarities = new ObservableCollection<Rarity>();
        private ObservableCollection<DisplayItem> items = new ObservableCollection<DisplayItem>();
        private App app = Application.Current as App;
        private TextBox searchBox;
        private ComboBox subtypesCB;

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
        public ObservableCollection<Rarity> Rarities
        {
            get { return rarities;  }
        }

        /// <summary>
        /// A collection of items that match the search parameters
        /// </summary>
        public ObservableCollection<DisplayItem> Items
        {
            get { return items; }
        }

        public MainPage()
        {
            try
            {
                types = new ObservableCollection<string>(app.controller.getTypesAsString());
                rarities = new ObservableCollection<Rarity>(app.controller.getRarities());
                types.Insert(0, "Any");
                subtypes.Add("Any");
                this.InitializeComponent();

                // Adjust width of hub sections according to screen resolution
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                SearchSection.Width = (Int32)(width * .9);
                WatchSection.Width = (Int32)(width * .9);
                GemSection.Width = width;
            }
            catch(Exception e)
            {
                showMessage();
            }
        }

        /// <summary>
        /// Store a reference to the ItemSearchBox once loaded
        /// </summary>
        /// <param name="sender">The ItemSearchBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void ItemSearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            searchBox = (TextBox)sender;
        }

        /// <summary>
        /// Sets the default index of the TypeCB ComboBox
        /// </summary>
        /// <param name="sender">The TypeCB ComboBox in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private void TypeCB_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the default index of the SubtypeCB ComboBox
        /// </summary>
        /// <param name="sender">The SubtypeCB ComboBox in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private void SubTypeCB_Loaded(object sender, RoutedEventArgs e)
        {
            subtypesCB = (ComboBox)sender;
            subtypesCB.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the default index of the RarityCB ComboBox
        /// </summary>
        /// <param name="sender">The RarityCB ComboBox in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private void RarityCB_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the width of the itemsListBox on load
        /// </summary>
        /// <param name="sender">The ItemsListBox in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private void ItemsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox itemsListBox = (ListBox)sender;
            double width = Window.Current.Bounds.Width;
            itemsListBox.Width = (Int32)(width * .83);
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
            if (lvlBox.Text.Equals(""))
            {
                lvlBox.Text = "Min Lvl";
                lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                try
                {
                    int lvl = Int32.Parse(lvlBox.Text);
                    app.controller.setMinLvl(lvl);
                    lvlBox.Text = app.controller.getMinLvl().ToString();
                }
                catch (FormatException)
                {
                    lvlBox.Text = "Min Lvl";
                    lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
                    app.controller.setMinLvl(0);
                }
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
            else
            {
                try
                {
                    int lvl = Int32.Parse(lvlBox.Text);
                    app.controller.setMaxLvl(lvl);
                    lvlBox.Text = app.controller.getMaxLvl().ToString();
                }
                catch (FormatException)
                {
                    lvlBox.Text = "Max Lvl";
                    lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
                    app.controller.setMaxLvl(80);
                }
            }
        }

        /// <summary>
        /// Event handler for when a type is selected from TypeCB. Populates the
        /// SubtypeCB from the possible Subtypes of the selected Type.
        /// </summary>
        /// <param name="sender">The TypeCB ComboBox in the Search hub section.</param>
        /// <param name="e">Event data</param>
        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox typeBox = (ComboBox)sender;
            app.controller.setType(typeBox.SelectedIndex - 1);
            List<string> list = app.controller.getSubtypesAsString();
            subtypes.Clear();
            subtypes.Add("Any");
            app.controller.setSubtype(-1);
            foreach(string subtype in list)
            {
                subtypes.Add(subtype);
            }
            if (subtypesCB != null)
            {
                subtypesCB.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Event handler for when a subtype is selected from SubtypeCB
        /// </summary>
        /// <param name="sender">The SubtypeCB ComboBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void SubTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox subtypeBox = (ComboBox)sender;
            if (subtypeBox.SelectedIndex != -1)
            {
                app.controller.setSubtype(subtypeBox.SelectedIndex - 1);
            }
            else
            {
                app.controller.setSubtype(subtypeBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Event handler for when a rarity is selected from RarityCB
        /// </summary>
        /// <param name="sender">The RarityCB ComboBox in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void RarityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox rarityBox = (ComboBox)sender;
            app.controller.setRarity(rarityBox.SelectedIndex - 1);
        }

        /// <summary>
        /// Gets the search text from ItemSearchBox and passes to the controller
        /// for a POST request. Stores the results in items List and displays
        /// them in the ItemsListBox.
        /// </summary>
        /// <param name="sender">The SearchBtn Button in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string itemName = searchBox.Text;
            if(!itemName.Equals("Item Name"))
            {
                items.Clear();
                try
                {
                    List<DisplayItem> results = app.controller.searchItems(itemName);
                    foreach (DisplayItem result in results)
                    {
                        items.Add(result);
                    }
                }
                catch(AggregateException exception)
                {
                    showMessage();
                }
            }
        }

        private async void showMessage()
        {
            var msg = new MessageDialog("Could not connect to www.gw2spidy.com.\nCheck your internet connection and try again.");
            msg.Title = "Oops!";
            await msg.ShowAsync();
            Application.Current.Exit();
        }
    }
}
