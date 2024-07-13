using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Chat;

namespace BiblioOnlineBookStore
{
    
    internal static class DataManagement
    {
        static SqlConnection _connection;
        static ObservableCollection<BookDTO> _books;
        public static ObservableCollection<BookDTO> _carts;
        public static User LoggedInUser;
        public static string UserType;
        static DataManagement()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"data source=<put your server name >;initial catalog=<database>;User id=<username>;password=<password>";
            _books = new ObservableCollection<BookDTO>();
            _carts = new ObservableCollection<BookDTO>();
        }

        public static ObservableCollection<BookDTO> GetAllBooks()
        {
            _books.Clear();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Books", _connection);
                sqlDataAdapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    BookDTO book = new BookDTO();
                    book.BookID = Convert.ToInt32(row["BookID"]);
                    book.Title = row["Title"].ToString();
                    book.AuthorName = GetAuthorName(Convert.ToInt32(row["AuthorID"]));
                    book.GenreName = GetGenreName(Convert.ToInt32(row["GenreID"]));
                    book.Price = Convert.ToDecimal(row["Price"]);
                    book.StockQuantity = Convert.ToInt32(row["StockQuantity"]);
                    book.PublicationYear = Convert.ToInt32(row["PublicationYear"]);
                    _books.Add(book);
                }
                return _books;

            }
            finally
            {
                _connection.Close();
            }
        }
        public static BookDTO GetBook(int bookid)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Books where bookid="+ bookid, _connection);
                sqlDataAdapter.Fill(dt);
                BookDTO bookDTO = null;
                foreach (DataRow row in dt.Rows)
                {
                    BookDTO book = new BookDTO();
                    book.BookID = Convert.ToInt32(row["BookID"]);
                    book.Title = row["Title"].ToString();
                    book.AuthorName = GetAuthorName(Convert.ToInt32(row["AuthorID"]));
                    book.GenreName = GetGenreName(Convert.ToInt32(row["GenreID"]));
                    book.Price = Convert.ToDecimal(row["Price"]);
                    book.StockQuantity = Convert.ToInt32(row["StockQuantity"]);
                    book.PublicationYear = Convert.ToInt32(row["PublicationYear"]);
                    bookDTO = book;
                }
                return bookDTO;

            }
            finally
            {
                _connection.Close();
            }
        }
        public static void Open()
        {
            SqlConnection sqlConnection = new SqlConnection(@"data source=<put your server name >;initial catalog=<database>;User id=<username>;password=<password>");
            sqlConnection.Open();
        }
        public static string GetAuthorName(int authorID)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select name from Authors where AuthorID=" + authorID;
                sqlCommand.Connection = _connection;
                _connection.Open();
                string name = sqlCommand.ExecuteScalar().ToString();
                return name;
            }
            finally
            {
                _connection.Close();    
            }
        }
        public static List<string> GetAllAuthorName()
        {
            try
            {
                List<string> authorName = new List<string>();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select name from Authors";
                sqlCommand.Connection = _connection;
                _connection.Open();
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                while (sdr.Read())
                {
                    authorName.Add(sdr[0].ToString());
                }

                return authorName;

            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<string> GetAllGenreName()
        {
            try
            {
                List<string> genreName = new List<string>();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select name from Genres";
                sqlCommand.Connection = _connection;
                _connection.Open();
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                while (sdr.Read())
                {
                    genreName.Add(sdr[0].ToString());
                }

                return genreName;

            }
            finally
            {
                _connection.Close();
            }
        }
        public static string GetGenreName(int genreID)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select name from Genres where GenreID=" + genreID;
                sqlCommand.Connection = _connection;
                _connection.Open();
                string name = sqlCommand.ExecuteScalar().ToString();
                return name;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static int GetAuthorID(string authorName)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select authorid from Authors where Name='" +authorName+"'" ;
                sqlCommand.Connection = _connection;
                _connection.Open();
                int id = int.Parse(sqlCommand.ExecuteScalar().ToString());
                return id;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static ObservableCollection<Author> GetAllAuthors()
        {
            ObservableCollection<Author> _authors = new ObservableCollection<Author>();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Authors", _connection);
                sqlDataAdapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Author author= new Author();
                    author.AuthorID = Convert.ToInt32(row["AuthorID"]);
                    author.Name= row["Name"].ToString();
                    author.Bio = row["Bio"].ToString();
                    author.Country = row["Country"].ToString();
                    _authors.Add(author);
                }
                return _authors;

            }
            finally
            {
                _connection.Close();
            }
        }
        public static int GetGenreID(string genreName)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select Genreid from Genres where Name='" + genreName + "'";
                sqlCommand.Connection = _connection;
                _connection.Open();
                int id = int.Parse(sqlCommand.ExecuteScalar().ToString());
                return id;
            }
            finally
            {
                _connection.Close();
            }
        }

        
        public static bool AddBook(BookDTO book)
        {
            try
            {
               
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "insert into Books(Title,AuthorID,GenreID,Price,StockQuantity,PublicationYear) values(@title,@authorID,@genreID,@price,@stockQty,@pubYear)";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@title", book.Title);
                parameters[1] = new SqlParameter("@authorID", GetAuthorID(book.AuthorName));
                parameters[2] = new SqlParameter("@genreID", GetGenreID(book.GenreName));
                parameters[3] = new SqlParameter("@price", book.Price);
                parameters[4] = new SqlParameter("@stockQty", book.StockQuantity);
                parameters[5] = new SqlParameter("@pubYear", book.PublicationYear);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool UpdateBook(BookDTO book)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Update Books set Title=@title,AuthorID=@authorID,GenreID=@genreid,Price=@price,StockQuantity=@stockQty,PublicationYear=@pubYear where BookID=@bookid";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@title", book.Title);
                parameters[1] = new SqlParameter("@authorID", GetAuthorID(book.AuthorName));
                parameters[2] = new SqlParameter("@genreID", GetGenreID(book.GenreName));
                parameters[3] = new SqlParameter("@price", book.Price);
                parameters[4] = new SqlParameter("@stockQty", book.StockQuantity);
                parameters[5] = new SqlParameter("@pubYear", book.PublicationYear);
                parameters[6] = new SqlParameter("@bookid", book.BookID);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool DeleteBook(BookDTO book)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "delete from  Books where BookID="+book.BookID;
                sqlCommand.Connection = _connection;
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool AddAuthor(Author author)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "insert into Authors(Name,Bio,Country) values(@name,@bio,@country)";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@name", author.Name);
                parameters[1] = new SqlParameter("@bio", author.Bio);
                parameters[2] = new SqlParameter("@country", author.Country);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool UpdateAuthor(Author author)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Update Authors set Name=@name,Bio=@bio,Country=@country where AuthorID=@authorid";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@name", author.Name);
                parameters[1] = new SqlParameter("@bio", author.Bio);
                parameters[2] = new SqlParameter("@country",author.Country);
                parameters[3] = new SqlParameter("@authorid", author.AuthorID);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool DeleteAuthor(Author author)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "delete from  Authors where AuthorID=" + author.AuthorID;
                sqlCommand.Connection = _connection;
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static void SubmitOrders()
        {
            try
            {
                decimal totalAmount = 0;
                foreach(var c in _carts )
                {
                    totalAmount += c.QuantityBought * c.Price;
                }
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "insert into Orders(OrderDate,CustomerID,TotalAmount) values(@OrderDate,@CustomerID,@TotalAmount)";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@OrderDate", DateTime.Today);
                if (LoggedInUser !=null )
                    parameters[1] = new SqlParameter("@CustomerID", LoggedInUser.UserID);
                else
                    parameters[1] = new SqlParameter("@CustomerID", DBNull.Value);
                parameters[2] = new SqlParameter("@TotalAmount", totalAmount);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select max(OrderID) from Orders";
                int OrderID = int.Parse(sqlCommand.ExecuteScalar().ToString());
                sqlCommand.CommandText = "insert into OrderDetails(OrderID,BookID,Quantity,Price) values(@OrderID,@BookID,@Quantity,@Price)";
                sqlCommand.Parameters.Clear();
                parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@OrderID", SqlDbType.Int);
                parameters[1] = new SqlParameter("@BookID",SqlDbType.Int);
                parameters[2] = new SqlParameter("@Quantity", SqlDbType.Int);
                parameters[3] = new SqlParameter("@Price", SqlDbType.Decimal);
                sqlCommand.Parameters.AddRange(parameters);
                foreach (var c in _carts)
                {
                    parameters[0].Value = OrderID;
                    parameters[1].Value = c.BookID;
                    parameters[2].Value = c.QuantityBought;
                    parameters[3].Value = c.Price;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch
            {

            }
            finally
            {
                _connection.Close();
                _carts.Clear();
            }
        }

        public static ObservableCollection<BookDTO> GetMyCart()
        {
            return _carts;
        }
        public static void AddToMyCart(ObservableCollection<BookDTO> mycart)
        {
            _carts = mycart;
        }


        public static int RegisterCustomer(Customer customer)
        {
            try
            {
                
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "insert into Customers(FullName,Email,Address,Phone) values(@name,@email,@address,@phone)";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@name", customer.FullName);
                parameters[1] = new SqlParameter("@email",customer.Email);
                parameters[2] = new SqlParameter("@address", customer.Address);
                parameters[3] = new SqlParameter("@phone", customer.Phone);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select max(CustomerID) from Customers";
                int CustomerID = int.Parse(sqlCommand.ExecuteScalar().ToString());
                sqlCommand.CommandText = "insert into Users values(@userid,@password,@type)";
                parameters[0] = new SqlParameter("@userid", CustomerID);
                parameters[1] = new SqlParameter("@password", customer.Password);
                parameters[2] = new SqlParameter("@type", "Member");
                sqlCommand.Parameters.AddRange(parameters);
                sqlCommand.ExecuteNonQuery();
                return CustomerID;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static User Login(int id, string password)
        {
            try
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select count(userid) from users where userid=@id and userpassword=@pass";
                sqlCommand.Connection = _connection;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@id", id);
                parameters[1] = new SqlParameter("@pass", password);
                sqlCommand.Parameters.AddRange(parameters);
                _connection.Open();
                int cuser=int.Parse(sqlCommand.ExecuteScalar().ToString());
                if (cuser > 0)
                {

                    
                    sqlCommand.CommandText = "select * from users where userid="+id;
                    sqlCommand.Parameters.Clear();
                    SqlDataReader sdr= sqlCommand.ExecuteReader();  
                    while (sdr.Read())
                    {
                        User user = new User() { UserID = int.Parse(sdr[0].ToString()), UserPassword = sdr[1].ToString(), UserType = sdr[2].ToString() };
                        LoggedInUser = user;
                        return user;
                    }
                }
                return null;
                
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
