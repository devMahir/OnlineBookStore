using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
    public sealed partial class CustomerRegistration : Page
    {
        public CustomerRegistration()
        {
            this.InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                Customer customer = new Customer();
                customer.FullName = txtName.Text;
                customer.Email = txtEmail.Text;
                customer.Address = txtAddress.Text;
                customer.Phone = txtPhone.Text;
                customer.Password = txtPassword.Password;
                int id = DataManagement.RegisterCustomer(customer);
                message.Text = "Your ID is " + id + ". Kindly Login With this ID";
            }

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Login));
        }

        public bool Validate()
        {
            validName.Text = "";
            validAddress.Text = "";
            validEmail.Text = "";
            validPhone.Text = "";
            validPassword.Text = "";
            validCPassword.Text = "";
            bool IsValid = true;
            if(txtName.Text.Trim().Length==0)
            {
                validName.Text = "Name is required.";
                IsValid = false;
            }
            if (txtAddress.Text.Trim().Length == 0)
            {
                validAddress.Text = "Address is required.";
                IsValid = false;
            }
            if (txtEmail.Text.Trim().Length==0)
            {
                validEmail.Text = "Email is required.";
                IsValid = false;
            }
            else if (!ValidEmail(txtEmail.Text))
            {
                validEmail.Text = "Not a valid email";
                IsValid = false;
            }
           
            if(txtPassword.Password.Trim().Length==0)
            {
                validPassword.Text = "Password is required";
                IsValid = false;
            }
            if (txtCPassword.Password.Trim().Length == 0)
            {
                validCPassword.Text = "Confirm Password is required";
                IsValid = false;
            }
            if(txtPhone.Text.Trim().Length==0)
            {
                validPhone.Text = "Phone is required";
                IsValid = false;
            }
            else if (!ValidPhone(txtPhone.Text))
            {
                validPhone.Text = "Not a valid phone number";
                IsValid = false;
            }


            return IsValid;
        }
        public bool ValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            // Create a Regex object
            Regex regex = new Regex(emailPattern);

            // Use the IsMatch method to check if the email matches the pattern
            return regex.IsMatch(email);
        }
        public bool ValidPhone(string phone)
        {

            string pattern = @"^[0-9]{10}$";

            Regex regex = new Regex(pattern);

         
            return regex.IsMatch(phone);
        }
    }
}
