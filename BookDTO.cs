using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioOnlineBookStore
{
    internal class BookDTO
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int PublicationYear { get; set; }
        public int QuantityBought { get; set; }
        // Constructors
        public BookDTO() { }

        public BookDTO(string title, string authorName, string genreName, decimal price, int stockQuantity, int publicationYear)
        {
            Title = title;
            AuthorName = authorName;
            GenreName = genreName;
            Price = price;
            StockQuantity = stockQuantity;
            PublicationYear = publicationYear;
        }
    }
}
