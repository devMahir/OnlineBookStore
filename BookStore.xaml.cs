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
    /// 

    
    public sealed partial class BookStore : Page
    {
        ObservableCollection<BookDTO> books;
        ObservableCollection<BookDTO> carts= new ObservableCollection<BookDTO>();
        List<Cart> cartList = new List<Cart>();
        public BookStore()
        {
            this.InitializeComponent();
            books = DataManagement.GetAllBooks();
            myGridView.ItemsSource = books;
            carts = DataManagement.GetMyCart();
            if (carts != null)
            {
                foreach (var c in carts)
                {
                    BookDTO bookDTO = books.First(b => b.BookID == c.BookID);
                    if (bookDTO != null)
                    {
                        bookDTO.QuantityBought = c.QuantityBought;
                    }
                }
            }
            if(DataManagement.LoggedInUser!=null)
            {
                loggedUser.Text = "User with user id " + DataManagement.LoggedInUser.UserID + " is logged in";
                LogOut.Visibility = Visibility.Visible;
            }
        }

        private void Click_Click(object sender, RoutedEventArgs e)
        {
            carts.Clear();
            foreach(var b in books)
            {
                if(b.QuantityBought>0)
                {
                    carts.Add(b);
                }
            }
            if (carts.Count > 0)
            {
                DataManagement.AddToMyCart(carts);
                this.Frame.Navigate(typeof(Cart));
            }
            else
            {
                message.Text = "Please add an item to cart";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AddToCart_Checked(object sender, RoutedEventArgs e)
        {
            

        }

        private void NoofItems_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            
                Button b = ((Button)sender);
                string id = b.Tag.ToString();
                BookDTO bookDTO = books.First(r => r.BookID == int.Parse(id));
                if (bookDTO != null)
                {
                    bookDTO.QuantityBought++;
                    myGridView.ItemsSource = null;
                    myGridView.ItemsSource = books;
                }


           
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button b = ((Button)sender);
            string id = b.Tag.ToString();
            BookDTO bookDTO = books.First(r => r.BookID == int.Parse(id));
            if (bookDTO != null && bookDTO.QuantityBought>0)
            {
                bookDTO.QuantityBought--;
                myGridView.ItemsSource = null;
                myGridView.ItemsSource = books;
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            DataManagement.LoggedInUser = null;
            DataManagement._carts.Clear();
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
