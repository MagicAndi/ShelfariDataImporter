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
            const string methodName = "Import";
            logger.Trace(LogHelper.BuildMethodEntryTrace(methodName));

            try
            {
                var records = new List<ShelfariRecord>();

                if (IsValidFile())
                {
                    using (TextReader reader = File.OpenText(inputFilePath))
                    {
                        using (var csv = new CsvReader(reader))
                        {
                            csv.Configuration.RegisterClassMap<ShelfariRecordMap>();
                            csv.Configuration.IgnoreHeaderWhiteSpace = true;
                            records = csv.GetRecords<ShelfariRecord>().ToList();
                        }
                    }
                }
                               
                using (var dbContext = new Model.ShelfariModel())
                {
                    dbContext.Books.AddRange(records);
                    dbContext.SaveChanges();
                }

                logger.Info(string.Format("Successfully imported {0} records.", records.Count));
            }
            catch (Exception ex)
            {
                logger.Error(ex, string.Format("Error importing file '{0}': {1}.", inputFilePath, ex.Message));
            }

            logger.Trace(LogHelper.BuildMethodExitTrace(methodName));
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