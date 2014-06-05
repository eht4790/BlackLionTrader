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

        public ObservableCollection<string> Types
        {
            get { return types; }
        }

        private ObservableCollection<string> subTypes = new ObservableCollection<string>();

        public ObservableCollection<string> SubTypes
        {
            get { return subTypes; }
        }

        private ObservableCollection<string> rarities = new ObservableCollection<string>();

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
        }

        private void ItemSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if(searchBox.Text.Equals("Item Name"))
            {
                searchBox.Text = string.Empty;
                searchBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void LevelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if(lvlBox.Text.Equals("Min Lvl") || lvlBox.Text.Equals("Max Lvl"))
            {
                lvlBox.Text = string.Empty;
                lvlBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void ItemSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if(searchBox.Text.Equals(""))
            {
                searchBox.Text = "Item Name";
                searchBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void MinLevelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if(lvlBox.Text.Equals(""))
            {
                lvlBox.Text = "Min Lvl";
                lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void MaxLevelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox lvlBox = (TextBox)sender;
            if (lvlBox.Text.Equals(""))
            {
                lvlBox.Text = "Max Lvl";
                lvlBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
