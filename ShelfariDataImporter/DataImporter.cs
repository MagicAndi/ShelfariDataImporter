using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using NLog;

using ShelfariDataImporter.Model;

namespace ShelfariDataImporter
{
    public class DataImporter
    {
        #region Private Data

        private string inputFilePath;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public Properties

        #endregion

        #region Constructor

        public DataImporter(string filePath)
        {
            inputFilePath = filePath;
        }

        #endregion

        #region Methods

        public void Import()
        {
            try
            {
                var records = new List<ShelfariRecord>();

                if (IsValidFile())
                {
                    using (TextReader reader = File.OpenText(inputFilePath))
                    {
                        using (var csv = new CsvReader(reader))
                        {
                            // csv.Configuration.RegisterClassMap<ShelfariRecordMap>();
                            csv.Configuration.IgnoreHeaderWhiteSpace = true;
                            records = csv.GetRecords<ShelfariRecord>().ToList();
                        }

                        //while (csv.Read())
                        //{
                        //    //var bookTitle = csv.GetField(0);
                        //    //Console.WriteLine(bookTitle);
                        //    var record = csv.GetRecord<ShelfariRecord>();
                        //    logger.Info(record.ToString());
                        //    records.Add(record);
                        //}
                    }
                }

                logger.Info(string.Format("Successfully imported {0} records.", records.Count));

                var sortedRecords = records.OrderByDescending(o => o.DateAdded).ToList();

                foreach (var record in sortedRecords)
                {
                    logger.Info(record.GetDetails());
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, string.Format("Error importing file '{0}': {1}.", inputFilePath, ex.Message));
            }
        }

        private bool IsValidFile()
        {
            bool isValid = false;

            if (File.Exists(inputFilePath))
            {
                isValid = true;
            }

            return isValid;
        }


        #endregion
    }
}
