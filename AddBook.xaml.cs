using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AddBook : Page
    {
        List<string> Author = new List<string>();
        List<string> Genres = new List<string>();
        public AddBook()
        {
            this.InitializeComponent();
            cmbAuthor.ItemsSource = DataManagement.GetAllAuthorName();
            cmbGenre.ItemsSource = DataManagement.GetAllGenreName();
           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (validationCheck())
            {
                string title = txtTitle.Text;
                string authorName = cmbAuthor.SelectedItem.ToString();
                string genreName = cmbGenre.SelectedItem.ToString();
                decimal price = decimal.Parse(txtPrice.Text);
                int stockQty = int.Parse(txtPrice.Text);
                int pubYear = int.Parse(txtPubYear.Text);
                BookDTO bookDTO = new BookDTO() { Title = title, AuthorName = authorName, GenreName = genreName, Price = price, StockQuantity = stockQty, PublicationYear = pubYear };
                bool result = DataManagement.AddBook(bookDTO);
                if (result)
                {
                    txtMessage.Text = "Book Added Successfully";
                }
                else
                {
                    txtMessage.Text = "Error In Added Book";
                }
            }
            


        }

        public bool validationCheck()
        {
            validTitle.Text = "";
            validPrice.Text = "";
            validPubYear.Text = "";
            validStockQty.Text = "";
            validAuthor.Text = "";
            validGenre.Text = "";
           
            bool valid = true;
            if(txtTitle.Text.Trim().Length==0)
            {
                validTitle.Text = "Title is required";
                valid = false;

            }
            if (txtPrice.Text.Trim().Length == 0)
            {
                validPrice.Text = "Price is required";
                valid = false;

            }
            else if(!decimal.TryParse(txtPrice.Text,out decimal price))
            {
                validPrice.Text = "Price needs to be decimal";
                valid = false;
            }
            else if (decimal.Parse(txtPrice.Text)<=0)
            {
                validPrice.Text = "Price needs to be > 0";
                valid = false;
            }
            if (txtPubYear.Text.Trim().Length == 0)
            {
                validPubYear.Text = "Pub Year is required";
                valid = false;

            }
            else if(!int.TryParse(txtPrice.Text, out int pubYear))
            {
                validPubYear.Text = "Pub Year needs to be a valid year";
                valid = false;
            }
            else if(!(int.Parse(txtPubYear.Text)>=1901 && int.Parse(txtPubYear.Text)<= System.DateTime.Today.Year))
            {
                validPubYear.Text = "Pub Year needs to be between 1901 & currentYear";
                valid = false;
            }
            if(txtStockQty.Text.Trim().Length==0)
            {
                validStockQty.Text = "Stock Qty is required";
                valid = false;
            }
            else if(!int.TryParse(txtStockQty.Text, out int stockQty))
            {
                validStockQty.Text = "Stock Qty should be a number";
                valid = false;
            }
            else if (int.Parse(txtStockQty.Text)<=0)
            {
                validStockQty.Text = "Stock Qty should be >0";
                valid = false;
            }
            if(cmbAuthor.SelectedItem==null)
            {
                validAuthor.Text = "Please select an author";
                valid = false;
            }
            if (cmbGenre.SelectedItem == null)
            {
                validGenre.Text = "Please select a genre";
                valid = false;
            }

            return valid;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "";
            txtMessage.Text = "";
            txtPrice.Text = "";
            cmbAuthor.Text = "";
            cmbGenre.Text = "";
            txtStockQty.Text = "";
            txtPubYear.Text = "";
            cmbAuthor.SelectedItem = null;
            cmbGenre.SelectedItem = null;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDelete));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDelete));
        }
    }
}
