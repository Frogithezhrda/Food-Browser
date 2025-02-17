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
        LinkedList<string> WebPagesFront;
        LinkedList<string> WebPagesBack;
        bool _addToList = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        void LoadWebPages(string Link, bool addToList = true)
        {
            _addToList = addToList;
            Search.Text = Link;
            Chrome.Address = Link;
            if (addToList)
            {
                WebPagesBack.AddFirst(Link);
            }
        }
        void GoHome()
        {
            Search.Text = "google.com";
            Chrome.Address = "google.com";
        }
        void ToggleWebPages(string Option)
        {
            if(Option == "←")
            {
                if((WebPagesBack.Count) > 1)
                {
                    WebPagesFront.AddFirst(WebPagesBack.First());
                    WebPagesBack.RemoveFirst();
                    LoadWebPages(WebPagesBack.First(), false);
                }
            }
            else
            {
                if((WebPagesFront.Count != 0))
                {
                    LoadWebPages(WebPagesFront.First(), false);
                    WebPagesBack.AddFirst(WebPagesFront.First());
                    WebPagesFront.RemoveFirst();
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
            LoadWebPages(Search.Text, false);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebPagesBack = new LinkedList<string>();
            WebPagesFront = new LinkedList<string>();
            GoHome();
            Chrome.AddressChanged += Chrome_AddressChanged;
            LoadWebPages(Search.Text, false);
            _addToList = true;
        }


        private void Chrome_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Search.Text = e.NewValue.ToString();
                if(_addToList)
                {
                    WebPagesBack.AddFirst(Search.Text);
                }
            });
        }
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoadWebPages(Search.Text, false);
                _addToList = true;
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
