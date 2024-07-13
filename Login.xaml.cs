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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BiblioOnlineBookStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationCheck())
            {
                User logged = DataManagement.Login(int.Parse(txtUserID.Text), txtPassword.Password);
                if (logged != null)
                {
                    if (logged.UserType == "Member")
                    {
                        if (DataManagement.UserType == "Member")
                        {
                            this.Frame.Navigate(typeof(BookStore));
                        }
                        else
                        {
                            message.Text = "Login Failed....";
                        }
                    }
                    else if (logged.UserType == "Admin")
                    {
                        if (DataManagement.UserType == "Admin")
                        {
                            this.Frame.Navigate(typeof(AdminPage));
                        }
                        else
                        {
                            message.Text = "Login Failed....";

                        }
                    }
                }
                else
                {
                    message.Text = "Login Failed....";
                }
            }
        }

        private bool ValidationCheck()
        {
            validUserID.Text = "";
            validPassword.Text = "";
            bool isValid = true;
            if(txtUserID.Text.Trim().Length==0)
            {
                validUserID.Text = "UserID is required";
                isValid= false;
            }
            if (txtPassword.Password.Trim().Length == 0)
            {
                validPassword.Text = "Password is required";
                isValid = false;
            }
            return isValid;
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerRegistration));
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
