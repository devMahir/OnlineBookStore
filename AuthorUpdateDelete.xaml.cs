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
    public sealed partial class AuthorUpdateDelete : Page
    {
        public Author author;
        ObservableCollection<Author> authors;
        List<int> authorIDs = new List<int>();
        public AuthorUpdateDelete()
        {
            this.InitializeComponent();
            authors = DataManagement.GetAllAuthors();
            foreach(var a in authors)
            {
                authorIDs.Add(a.AuthorID);
            }
            cmbAuthorID.ItemsSource= authorIDs;

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            if(ValidationCheck())
            {
                bool isUpdated=DataManagement.UpdateAuthor(author);
                if (isUpdated)
                    txtMessage.Text = "Author updated successfully";
                else
                    txtMessage.Text = "Error in updating Author";

            }
        }
        public void Clear()
        {
            txtMessage.Text = "";
            validAuthorName.Text = "";
            validBio.Text = "";
            validCountry.Text = "";
        }

        public void ClearControls()
        {
            txtAuthorName.Text = "";
            txtBio.Text = "";
            txtCountry.Text = "";
           // cmbAuthorID.SelectedItem = null;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            Clear();
            if (ValidationCheck())
            {
                bool isDeleted = DataManagement.DeleteAuthor(author);
                if (isDeleted)
                {
                    txtMessage.Text = "Author deleted successfully";
                    authorIDs.Remove(author.AuthorID);
                   cmbAuthorID.ItemsSource= authorIDs;
                    ClearControls();
                   
                }
                else
                    txtMessage.Text = "Error in deleting Author";

            }
        }

        private void btnGoToAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddAuthor));
        }

        private void cmbAuthorID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            author = authors.First(a => a.AuthorID == int.Parse(cmbAuthorID.SelectedItem.ToString()));
            if(author!= null) 
            {
                txtAuthorName.Text = author.Name;
                txtBio.Text = author.Bio;
                txtCountry.Text = author.Country;

            }

        }
        public bool ValidationCheck()
        {
            bool IsValid = true;
            if(cmbAuthorID.SelectedItem==null)
            {
                txtMessage.Text = "Please select an Author ID";
            }
            if (txtAuthorName.Text.Trim().Length == 0)
            {
                validAuthorName.Text = "Author Name is required";
                IsValid = false;
            }
            if (txtBio.Text.Trim().Length == 0)
            {
                validBio.Text = "Bio is required";
                IsValid = false;
            }
            if (txtCountry.Text.Trim().Length == 0)
            {
                validCountry.Text = "Country is required";
                IsValid = false;
            }

            return IsValid;


        }

        private void btnGoToAdminPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }
    }
}
