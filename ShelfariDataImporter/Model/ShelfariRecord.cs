using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShelfariDataImporter.Model
{
    /// <summary>
    /// A POCO class to hold the Shelfari record data
    /// </summary>
    public class ShelfariRecord
    {
        #region Public Properties

        public int ID { get; set; }
        public string Title { get; set; }
        public string AdditionalAuthors { get; set; }
        public string ISBN { get; set; }
        public string MyRating { get; set; }
        public string Publisher { get; set; }
        public string Binding { get; set; }
        public string YearPublished { get; set; }
        public string DateRead { get; set; }
        public string DateAdded { get; set; }
        public string Bookshelves { get; set; }
        public string MyReview { get; set; }
        public string Author { get; set; }
        public string Owned { get; set; }
        public string Favorite { get; set; }
        public string Wishlist { get; set; }
        public string PlanToRead { get; set; }
        public string CurrentlyReading { get; set; }
        public string Read { get; set; }
        public string ASIN { get; set; }
        public string ShelfariEditionId { get; set; }
        public string ShelfariBookId { get; set; }
        public string Condition { get; set; }
        public string OriginalPurchaseDate { get; set; }
        public string PurchasePrice { get; set; }
        public string PrivateNotes { get; set; }
        public string Signed { get; set; }
        public string Loaned { get; set; }
        public string LoanedTo { get; set; }
        public string LoanedOn { get; set; }
        public string LoanDue { get; set; }
        public string IsPrivate { get; set; }

        #endregion

        #region Constructor(s)

        public ShelfariRecord()
        {
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            var values = new List<string>();

            foreach (var prop in this.GetType().GetProperties())
            {
                var propertyName = prop.Name;
                var propertyValue = prop.GetValue(this, null);
                if (propertyValue == null)
                {
                    propertyValue = "NULL";
                }

                values.Add(string.Format("{0} = {1}", propertyName, propertyValue));
            }

            return string.Join<string>(", ", values);
        }

        public string GetDetails()
        {
            var details = new StringBuilder();

            details.Append(string.Format("Title: '{0}'", this.Title));
            details.Append(string.Format(", Author: '{0}'", this.Author));
            details.Append(string.Format(", Additional Authors: '{0}'", this.AdditionalAuthors));
            details.Append(string.Format(", My Rating: '{0}'", this.MyRating));
            details.Append(string.Format(", Year Published: '{0}'", this.YearPublished));
            details.Append(string.Format(", Date Added: '{0}'", this.DateAdded));
            details.Append(string.Format(", Date Read: '{0}'", this.DateRead));
            details.Append(string.Format(", Bookshelves: '{0}'", this.Bookshelves));
            details.Append(string.Format(", My Review: '{0}'", this.MyReview));
            details.Append(string.Format(", Plan To Read: '{0}'", this.PlanToRead));
            details.Append(string.Format(", Currently Reading: '{0}'", this.CurrentlyReading));
            details.Append(string.Format(", Read: '{0}'", this.Read));

            return details.ToString();
        }

        #endregion
    }
}