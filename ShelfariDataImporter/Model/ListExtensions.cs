using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfariDataImporter.Model
{
    /// <summary>
    /// Holds extension methods for the various list collections
    /// </summary>
    public static class ListExtensions
    {
        public static List<Book> ConvertToBooks(this List<ShelfariRecord> records)
        {
            var books = new List<Book>();

            foreach (var record in records)
            {
                books.Add(new Book(record));
            }

            return books;
        }

    }
}