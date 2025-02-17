using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CefSharp;

namespace FOODXDBROWSER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinkedList<string> WebPagesFront = new LinkedList<string>();
        LinkedList<string> WebPagesBack = new LinkedList<string>();
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
                if (WebPagesBack.Count == 0 || WebPagesBack.First() != Link)
                {
                    WebPagesBack.AddFirst(Link);
                }
            }
        }
        void GoHome()
        {
            WebPagesFront.Clear();
            WebPagesBack.Clear();
            LoadWebPages("https://www.google.com");
        }
        void ToggleWebPages(string Option)
        {
            if(Option == "←" && WebPagesBack.Count > 1)
            {

                    WebPagesFront.AddFirst(WebPagesBack.First());
                    WebPagesBack.RemoveFirst();
                    LoadWebPages(WebPagesBack.First());
            }
            else if (WebPagesFront.Count != 0)
            {

                string nextPage = WebPagesFront.First();
                WebPagesFront.RemoveFirst();
                LoadWebPages(nextPage);
            }
        }
        private void UpdateNavigationButtons()
        {
            Back.FontWeight = (WebPagesBack.Count > 1) ? FontWeights.Bold : FontWeights.Normal;
            Back.Foreground = (WebPagesBack.Count > 1) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.DarkSlateGray);

            Front.FontWeight = (WebPagesFront.Count > 0) ? FontWeights.Bold : FontWeights.Normal;
            Front.Foreground = (WebPagesFront.Count > 0) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.DarkSlateGray);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ToggleWebPages(button.Content.ToString());
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Chrome.Reload();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GoHome();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Chrome.AddressChanged += Chrome_AddressChanged;
            GoHome();
        }


        private void Chrome_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //invoke when the address is changed
            Dispatcher.Invoke(() =>
            {
                //getting the value to thee text
                Search.Text = e.NewValue.ToString();
                if (_addToList && (WebPagesBack.Count == 0 || WebPagesBack.First() != Search.Text))
                {
                    WebPagesBack.AddFirst(Search.Text);
                    WebPagesFront.Clear();
                }
                UpdateNavigationButtons();
            });
        }
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            //when key enter is down
            if(e.Key == Key.Enter)
            {
                //load the page and add it
                LoadWebPages(Search.Text, true);
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        }
        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;   
        }



        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
