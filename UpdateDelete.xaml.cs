using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    public sealed partial class UpdateDelete : Page
    {
         BookDTO bookDTO = null;
        ObservableCollection<int> bookids= new ObservableCollection<int>();
        string flag = "update";
        public UpdateDelete()
        {
            this.InitializeComponent();
            cmbAuthor.ItemsSource = DataManagement.GetAllAuthorName();
            cmbGenre.ItemsSource = DataManagement.GetAllGenreName();
            UpdateBookID();
            ReadOnly(true);
        }
        
        public void UpdateBookID()
        {
            bookids.Clear();
            cmbBookID.ItemsSource = bookids;
            
            List<BookDTO> books = DataManagement.GetAllBooks().ToList<BookDTO>();
            foreach (var b in books)
            {
                //cmbBookID.Items.Add(b.BookID);
                bookids.Add(b.BookID);
            }
            cmbBookID.ItemsSource= bookids;
        }
        public void ReadOnly(bool enable)
        {
                txtTitle.IsReadOnly = enable;
                txtMessage.Text = "";
                txtPrice.IsReadOnly = enable;
                txtPubYear.IsReadOnly = enable;    
                txtStockQty.IsReadOnly = enable;
                cmbAuthor.IsEnabled = !enable;
                cmbGenre.IsEnabled = !enable;               
                
           
        }

        private void cmbBookID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag == "update")
            {
                bookDTO = DataManagement.GetBook(int.Parse(cmbBookID.SelectedValue.ToString()));
                Fill();
               
            }
            //else
            //{
            //    flag = "update";
            //}
        }

        private void Fill()
        {
            if(bookDTO!=null)
            {
                txtTitle.Text = bookDTO.Title;
                txtPrice.Text = bookDTO.Price.ToString();
                txtPubYear.Text=bookDTO.PublicationYear.ToString(); 
                txtStockQty.Text=bookDTO.StockQuantity.ToString();
                cmbAuthor.SelectedItem = bookDTO.AuthorName;
                cmbGenre.SelectedItem=bookDTO.GenreName;
                ReadOnly(false);
            } 
        }
        private void Clear()
        {
            
                txtTitle.Text = "";
                txtPrice.Text = "";
            txtPubYear.Text = "";
            txtStockQty.Text = "";
            cmbAuthor.SelectedItem =null;
             cmbGenre.SelectedItem = null;
              ReadOnly(true);
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            if (validationCheck())
            {
                bool result = DataManagement.UpdateBook(bookDTO);
                if (result)
                {
                    txtMessage.Text = "Update Successfully";
                    Clear();
                }
                else
                    txtMessage.Text = "Error..Updating Book";
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            bool result = DataManagement.DeleteBook(bookDTO);
            if (result)
            {
                txtMessage.Text = "Delete Successfully";
                flag = "delete";
                bookids.Remove(bookDTO.BookID);
                flag = "update";
                Clear();
            }
            else
                txtMessage.Text = "Error..Deleting Book";
        }

        private void btnGoToAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddBook));
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
            if(cmbBookID.SelectedItem==null)
            {
                txtMessage.Text = "Please select a book to update or delete";
                valid = false;
            }
            if (txtTitle.Text.Trim().Length == 0)
            {
                validTitle.Text = "Title is required";
                valid = false;

            }
            if (txtPrice.Text.Trim().Length == 0)
            {
                validPrice.Text = "Price is required";
                valid = false;

            }
            else if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                validPrice.Text = "Price needs to be decimal";
                valid = false;
            }
            else if (decimal.Parse(txtPrice.Text) <= 0)
            {
                validPrice.Text = "Price needs to be > 0";
                valid = false;
            }
            if (txtPubYear.Text.Trim().Length == 0)
            {
                validPubYear.Text = "Pub Year is required";
                valid = false;

            }
            else if (!int.TryParse(txtPubYear.Text, out int pubYear))
            {
                validPubYear.Text = "Pub Year needs to be a valid year";
                valid = false;
            }
            else if (!(int.Parse(txtPubYear.Text) >= 1901 && int.Parse(txtPubYear.Text) <= System.DateTime.Today.Year))
            {
                validPubYear.Text = "Pub Year needs to be between 1901 & currentYear";
                valid = false;
            }
            if (txtStockQty.Text.Trim().Length == 0)
            {
                validStockQty.Text = "Stock Qty is required";
                valid = false;
            }
            else if (!int.TryParse(txtStockQty.Text, out int stockQty))
            {
                validStockQty.Text = "Stock Qty should be a number";
                valid = false;
            }
            else if (int.Parse(txtStockQty.Text) <= 0)
            {
                validStockQty.Text = "Stock Qty should be >0";
                valid = false;
            }
            if (cmbAuthor.SelectedItem == null)
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

        private void btnGoToAdminPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }
    }
}
