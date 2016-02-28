using System;
using System.Collections.Generic;

namespace ShelfariFileConverter.Model
{
    public class Book
    {
        #region Public Properties
        
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int MyRating { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateAdded { get; set; }
        public string Tags { get; set; }
        public string MyReview { get; set; }        
        public bool PlanToRead { get; set; }
        public bool CurrentlyReading { get; set; }
        public bool Read { get; set; }

        public int YearRead
        {
            get
            {
                if (DateRead.HasValue)
                {
                    return DateRead.Value.Year;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Constructor(s)

        public Book(ShelfariRecord record)
        {
            Title = record.Title;
            Author = record.Author;
            ISBN = record.ISBN;
            MyRating = record.MyRating;
            DateRead = record.DateRead;
            DateAdded = record.DateAdded;
            Tags = record.Bookshelves;
            MyReview = record.MyReview;
            PlanToRead = record.PlanToRead;
            CurrentlyReading = record.CurrentlyReading;
            Read = record.Read;
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

        #endregion
    }
}
