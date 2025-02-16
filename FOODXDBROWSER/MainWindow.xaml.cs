using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp.Wpf;

namespace FOODXDBROWSER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> WebPages;
        int current = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        void LoadWebPages(string Link, bool addToList = true)
        {
            string LastLink = "";

            Chrome.Address = Link;
            Search.Text = Link;
            LastLink = Link;
            
            
            if (addToList)
            {
                current++;
                WebPages.Add(Link);

            }
        }
        void GoHome()
        {
            Search.Text = "google.com";
            Chrome.Address = "google.com";
            WebPages.Add("google.com");
        }
        void ToggleWebPages(string Option)
        {
            if(Option == "→")
            {
                if((WebPages.Count-current-1) != 0)
                {
                    current++;
                    LoadWebPages(WebPages[current], false);
                }
            }
            else
            {
                if((WebPages.Count + current - 1) >= WebPages.Count)
                {
                    current--;
                    LoadWebPages(WebPages[current], false);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ToggleWebPages(button.Content.ToString());
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[current]);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[0]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebPages = new List<string>();
            GoHome();
            LoadWebPages(Search.Text);
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoadWebPages(Search.Text);
                LoadWebPages(Chrome.Address);

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        }
        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;   
        }

        private void Browser_Click(object sender, RoutedEventArgs e)
        {

            if(Search.Text == "google.com")
            {
                Search.Text = "search.brave.com";
                LoadWebPages(Search.Text);
            }
            else
            {
                Search.Text = "google.com";
                LoadWebPages(Search.Text);
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HistoryPart_Click(object sender, RoutedEventArgs e)
        {
            LoadWebPages(Chrome.Address);
        }

       

        private void DarkMode_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
