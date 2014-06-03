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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlackLionTrader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
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
