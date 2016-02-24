using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using NLog;

using ShelfariDataImporter.EntityFramework;
using ShelfariDataImporter.Model;
using ShelfariDataImporter.Utilities;

namespace ShelfariDataImporter
{
    public class DataImporter
    {
        #region Private Data

        private string inputFilePath;
        private string outputFolder;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public Properties

        #endregion

        #region Constructor

        public DataImporter(string filePath, string outputDirectory)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(string.Format("The input file '{0}' does not exist.", filePath), "filePath");
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            inputFilePath = filePath;
            outputFolder = outputDirectory;

            if (!outputFolder.EndsWith("\\"))
            {
                outputFolder += "\\";
            }
        }

        #endregion

        #region Methods

        public void Import()
        {
            const string methodName = "Import";
            logger.Trace(LogHelper.BuildMethodEntryTrace(methodName));

            try
            {
                var records = new List<ShelfariRecord>();
                
                using (TextReader reader = File.OpenText(inputFilePath))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.RegisterClassMap<ShelfariRecordMap>();
                        csv.Configuration.IgnoreHeaderWhiteSpace = true;
                        records = csv.GetRecords<ShelfariRecord>().ToList();
                    }
                }

                if (records.Any())
                {
                    var readRecords = records.Where(b => b.Read = true).ToList<ShelfariRecord>();
                    var readBooks = readRecords.ConvertToBooks();
                    readBooks = readBooks.OrderByDescending(b => b.DateRead).ToList();

                    using (TextWriter writer = File.CreateText(@"C:\Users\MagicAndi\Dropbox\Backups\Shelfari\Read.csv"))
                    {
                        using (var csv = new CsvWriter(writer))
                        {
                            // csv.WriteRecords(records);
                            csv.Configuration.Encoding = Encoding.UTF8;
                            csv.WriteHeader<Book>();
                            foreach (var record in readBooks)
                            {
                                csv.WriteRecord(record);
                            }
                        }
                    }

                    logger.Info(string.Format("Successfully imported {0} records.", records.Count));
                }

                //using (var dbContext = new Model.ShelfariModel())
                //{
                //    dbContext.Books.AddRange(records);
                //    dbContext.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex, string.Format("Error importing file '{0}': {1}.", inputFilePath, ex.Message));
            }

            logger.Trace(LogHelper.BuildMethodExitTrace(methodName));
        }
        
        private void TruncateTables()
        {
            using (var dbContext = new Model.ShelfariModel())
            {
                var tablesToDelete = new string[] { "ShelfariRecords" };
                dbContext.Truncates(tablesToDelete);
                logger.Info("Successfully truncated tables in Shelfari database.");
            }
        }
        
        #endregion
    }
}