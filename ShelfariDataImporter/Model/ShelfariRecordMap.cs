using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace ShelfariDataImporter.Model
{
    public class ShelfariRecordMap : CsvClassMap<ShelfariRecord>
    {
        public ShelfariRecordMap()
        {
            Map(m => m.Title).Index(0);
            Map(m => m.AdditionalAuthors).Index(1);
            Map(m => m.AdditionalAuthors).Index(2);
            Map(m => m.ISBN).Index(3);
            Map(m => m.MyRating).Index(4);
            Map(m => m.Publisher).Index(5);
            Map(m => m.Binding).Index(6);
            Map(m => m.YearPublished).Index(7);
            Map(m => m.DateRead).Index(8);
            Map(m => m.DateAdded).Index(9);
            Map(m => m.Bookshelves).Index(10);
            Map(m => m.MyReview).Index(11);
            Map(m => m.Author).Index(12);
            Map(m => m.Owned).Index(13);
            Map(m => m.Favorite).Index(14);
            Map(m => m.Wishlist).Index(15);
            Map(m => m.PlanToRead).Index(16);
            Map(m => m.CurrentlyReading).Index(17);
            Map(m => m.Read).Index(18);
            Map(m => m.ASIN).Index(19);
            Map(m => m.ShelfariEditionId).Index(20);
            Map(m => m.ShelfariBookId).Index(21);
            Map(m => m.Condition).Index(22);
            Map(m => m.OriginalPurchaseDate).Index(23);
            Map(m => m.PurchasePrice).Index(24);
            Map(m => m.PrivateNotes).Index(25);
            Map(m => m.Signed).Index(26);
            Map(m => m.Loaned).Index(27);
            Map(m => m.LoanedTo).Index(28);
            Map(m => m.LoanedOn).Index(29);
            Map(m => m.LoanDue).Index(30);
            Map(m => m.IsPrivate).Index(31);
        }
    }
}
