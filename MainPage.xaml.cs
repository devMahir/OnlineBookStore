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
using System.Data.SqlClient;
using System.ServiceModel.Channels;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BiblioOnlineBookStore
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

       
        private void rbAdmin_Checked(object sender, RoutedEventArgs e)
        {
            if(rbAdmin.IsChecked==true)
            {
                this.Frame.Navigate(typeof(Login));
                DataManagement.UserType = "Admin";
            }
            else
            {

            }

        }

        private void rbMember_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMember.IsChecked == true)
            {
                this.Frame.Navigate(typeof(Login));
                DataManagement.UserType = "Member";
            }
            else
            {

            }
        }

        private void rbGuest_Checked(object sender, RoutedEventArgs e)
        {
            if (rbGuest.IsChecked == true)
            {
                this.Frame.Navigate(typeof(BookStore));
                DataManagement.UserType = "Guest";
            }
            else
            {

            }
        }
    }
}
