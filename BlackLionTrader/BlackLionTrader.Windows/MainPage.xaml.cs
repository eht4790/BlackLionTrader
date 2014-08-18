
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
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackLionTrader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Binding collections
        private ObservableCollection<string> types = new ObservableCollection<string>();
        private ObservableCollection<string> subtypes = new ObservableCollection<string>();
        private ObservableCollection<Rarity> rarities = new ObservableCollection<Rarity>();
        private ObservableCollection<DisplayItem> items = new ObservableCollection<DisplayItem>();

        // Reference to Application
        private App app = Application.Current as App;

        // References to UI Controls the DataContext
        private TextBox searchBox;
        private ProgressRing progressRing;
        private ComboBox subtypesCB;

        // Used to track the state of arrow images used in sorting by columns
        private enum Columns { Name, Lvl, Supply, Demand, MinSale, MaxBuy, Margin };
        private Columns prevSortColumn = Columns.Name;
        private bool sortAscending = true;

        // References to sort arrows images in the Search Hub section
        private Image nameArrow;
        private Image lvlArrow;
        private Image supplyArrow;
        private Image demandArrow;
        private Image minSaleArrow;
        private Image maxBuyArrow;
        private Image marginArrow;

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
        /// Store a reference to the ProgressRing
        /// </summary>
        /// <param name="sender">The ProgressRing in the Search hub section</param>
        /// <param name="e">Event data</param>
        private void ProgressRing_Loaded(object sender, RoutedEventArgs e)
        {
            progressRing = (ProgressRing)sender;
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
        /// Sets the width of the grid that stores the list labels
        /// to a fraction of the actual screen.
        /// </summary>
        /// <param name="sender">The grid of labels ListBoxLabels</param>
        /// <param name="e">Event Data</param>
        private void ListBoxLabels_Loaded(object sender, RoutedEventArgs e)
        {
            Grid listBoxLabels = (Grid)sender;
            double width = Window.Current.Bounds.Width;
            listBoxLabels.Width = (Int32)(width * .83);
        }

        /// <summary>
        /// Stores a reference to the NameArrow image used to mark that items
        /// are sorted by name.
        /// </summary>
        /// <param name="sender">The NameArrow Image</param>
        /// <param name="e">Event Data</param>
        private void NameArrow_Loaded(object sender, RoutedEventArgs e)
        {
            nameArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the LvlArrow image used to mark that items
        /// are sorted by level.
        /// </summary>
        /// <param name="sender">The LvlArrow Image</param>
        /// <param name="e">Event Data</param>
        private void LvlArrow_Loaded(object sender, RoutedEventArgs e)
        {
            lvlArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the SupplyArrow image used to mark that items
        /// are sorted by supply.
        /// </summary>
        /// <param name="sender">The SupplyArrow Image</param>
        /// <param name="e">Event Data</param>
        private void SupplyArrow_Loaded(object sender, RoutedEventArgs e)
        {
            supplyArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the DemandArrow image used to mark that items
        /// are sorted by demand.
        /// </summary>
        /// <param name="sender">The DemandArrow Image</param>
        /// <param name="e">Event Data</param>
        private void DemandArrow_Loaded(object sender, RoutedEventArgs e)
        {
            demandArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the MinSaleArrow image used to mark that items
        /// are sorted by min sale offer.
        /// </summary>
        /// <param name="sender">The MinSaleArrow Image</param>
        /// <param name="e">Event Data</param>
        private void MinSaleArrow_Loaded(object sender, RoutedEventArgs e)
        {
            minSaleArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the MaxBuyArrow image used to mark that items
        /// are sorted by max buy offer.
        /// </summary>
        /// <param name="sender">The MaxBuyArrow Image</param>
        /// <param name="e">Event Data</param>
        private void MaxBuyArrow_Loaded(object sender, RoutedEventArgs e)
        {
            maxBuyArrow = (Image)sender;
        }

        /// <summary>
        /// Stores a reference to the MarginArrow image used to mark that items
        /// are sorted by margin.
        /// </summary>
        /// <param name="sender">The MarginArrow Image</param>
        /// <param name="e">Event Data</param>
        private void MaringArrow_Loaded(object sender, RoutedEventArgs e)
        {
            marginArrow = (Image)sender;
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
            app.controller.setRarity(rarityBox.SelectedIndex);
        }

        /// <summary>
        /// Gets the search text from ItemSearchBox and passes to the controller
        /// for a POST request. Stores the results in items List and displays
        /// them in the ItemsListBox.
        /// </summary>
        /// <param name="sender">The SearchBtn Button in the Search hub section</param>
        /// <param name="e">Event Data</param>
        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            string itemName = searchBox.Text;
            if(!itemName.Equals("Item Name"))
            {
                items.Clear();
                List<DisplayItem> results = await Task.Run(() => runSearch(itemName));
                foreach(DisplayItem result in results)
                {
                    items.Add(result);
                }
            }
            else
            {
                items.Clear();
                List<DisplayItem> results = await Task.Run(() => runSearch(null));
                foreach(DisplayItem result in results)
                {
                    items.Add(result);
                }
            }
            progressRing.IsActive = false;
        }

        /// <summary>
        /// Sends a search call to the controller to find the list of items with the
        /// search conditions selected in the UI
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        private async Task<List<DisplayItem>> runSearch(string itemName)
        {
            try
            {
                List<DisplayItem> results = await app.controller.searchItems(itemName);
                return results;
            }
            catch (AggregateException exception)
            {
                showMessage();
                return null;
            }
        }

        /// <summary>
        /// Displays a MessageDialog to the user if an exception is thrown then exits the app
        /// </summary>
        private async void showMessage()
        {
            var msg = new MessageDialog("Could not connect to www.gw2spidy.com.\nCheck your internet connection and try again.");
            msg.Title = "Oops!";
            await msg.ShowAsync();
            Application.Current.Exit();
        }

        /// <summary>
        /// Sorts the list of items according to name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            nameArrow.Visibility = Visibility.Visible;
            sortBy(Columns.Name);
        }

        /// <summary>
        /// Sorts the list of items according to restriction level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvlLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lvlArrow.Visibility = Visibility.Visible;
            sortBy(Columns.Lvl);
        }

        /// <summary>
        /// Sorts the list of items according to supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupplyLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            supplyArrow.Visibility = Visibility.Visible;
            sortBy(Columns.Supply);
        }

        /// <summary>
        /// Sorts the list of items according to demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DemandLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            demandArrow.Visibility = Visibility.Visible;
            sortBy(Columns.Demand);
        }

        /// <summary>
        /// Sorts the list of items according to min sale offer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinSaleLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            minSaleArrow.Visibility = Visibility.Visible;
            sortBy(Columns.MinSale);
        }

        /// <summary>
        /// Sorts the list of items according to max buy offer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxBuyLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            maxBuyArrow.Visibility = Visibility.Visible;
            sortBy(Columns.MaxBuy);
        }

        /// <summary>
        /// Sorts the list of items according the difference between the min sale offer and the max buy offer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarginLabel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            marginArrow.Visibility = Visibility.Visible;
            sortBy(Columns.Margin);
        }

        /// <summary>
        /// Sorts the item list by the given column and marks the given column
        /// with the appropriate arrow to signify sorting
        /// </summary>
        /// <param name="column">The column that was selected for sorting</param>
        private void sortBy(Columns column)
        {
            List<DisplayItem> results = app.controller.sortByColumn((int)column);
            items.Clear();
            foreach(DisplayItem result in results)
            {
                items.Add(result);
            }

            if(column == prevSortColumn)
            {
                sortAscending = !sortAscending;
            }

            else
            {
                switch (prevSortColumn)
                {
                    case Columns.Name:
                        nameArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.Lvl:
                        lvlArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.Supply:
                        supplyArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.Demand:
                        demandArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.MinSale:
                        minSaleArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.MaxBuy:
                        maxBuyArrow.Visibility = Visibility.Collapsed;
                        break;

                    case Columns.Margin:
                        marginArrow.Visibility = Visibility.Collapsed;
                        break;

                    default:
                        break;
                }
                prevSortColumn = column;
            }

            switch(column)
            {
                case Columns.Name:
                    if(sortAscending)
                    {
                        nameArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        nameArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.Lvl:
                    if(sortAscending)
                    {
                        lvlArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        lvlArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.Supply:
                    if(sortAscending)
                    {
                        supplyArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        supplyArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.Demand:
                    if(sortAscending)
                    {
                        demandArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        demandArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.MinSale:
                    if(sortAscending)
                    {
                        minSaleArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        minSaleArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.MaxBuy:
                    if(sortAscending)
                    {
                        maxBuyArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        maxBuyArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                case Columns.Margin:
                    if(sortAscending)
                    {
                        marginArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/up.png", UriKind.Absolute));
                    }
                    else
                    {
                        marginArrow.Source = new BitmapImage(new Uri("ms-appx:/SharedAssets/down.png", UriKind.Absolute));
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Event Handler for tapping the watch/unwatch image in the list of search results.
        /// Makes a call to the controller to add/remove the item from the favorites list
        /// </summary>
        /// <param name="sender">The watch/unwatch Image that was tapped</param>
        /// <param name="e">Event Data</param>
        private void WatchImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }
    }
}
