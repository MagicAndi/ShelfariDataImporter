using System;
using System.Collections.Generic;
using System.Data;
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

        public static DataTable ConvertToDataTable(this List<ShelfariRecord> records, bool read = false)
        {
            DataTable table = new DataTable("Books");
            
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("Author", typeof(string)));
            table.Columns.Add(new DataColumn("ISBN", typeof(string)));
            table.Columns.Add(new DataColumn("DateAdded", typeof(DateTime)));

            if (read)
            {
                table.Columns.Add(new DataColumn("DateRead", typeof(DateTime)));
                table.Columns.Add(new DataColumn("YearRead", typeof(int)));
                table.Columns.Add(new DataColumn("Tags", typeof(string)));
                table.Columns.Add(new DataColumn("MyRating", typeof(int)));
                table.Columns.Add(new DataColumn("MyReview", typeof(string)));
            }

            foreach (var record in records)
            {
                DataRow row = table.NewRow();
                row["Title"] = record.Title;
                row["Author"] = record.Author;
                row["ISBN"] = record.ISBN;
                row["DateAdded"] = record.DateAdded;

                if (read)
                {
                    if (record.DateRead.HasValue)
                    {
                        row["DateRead"] = record.DateRead.Value;
                    }
                    else
                    {
                        row["DateRead"] = DBNull.Value;
                    }

                    if (record.DateRead.HasValue)
                    {
                        row["YearRead"] = record.DateRead.Value.Year;
                    }
                    else
                    {
                        row["YearRead"] = DBNull.Value;
                    }

                    row["Tags"] = record.Bookshelves;
                    row["MyRating"] = record.MyRating;
                    row["MyReview"] = record.MyReview;
                }

                table.Rows.Add(row);
            }

            return table;
        }
    }
}