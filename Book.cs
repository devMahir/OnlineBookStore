using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiblioOnlineBookStore
{
    internal class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int GenreID { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublicationYear { get; set; }

        // Constructors
        public Book() { }

        public Book(string title, int authorID, int genreID, decimal price, int stockQuantity, int publicationYear)
        {
            Title = title;
            AuthorID = authorID;
            GenreID = genreID;
            Price = price;
            StockQuantity = stockQuantity;
            PublicationYear = publicationYear;
        }
    }
}
