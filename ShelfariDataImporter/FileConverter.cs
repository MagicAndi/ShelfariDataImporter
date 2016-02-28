using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using NLog;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using ShelfariDataImporter.Model;
using ShelfariDataImporter.Utilities;

namespace ShelfariDataImporter
{
    public class FileConverter
    {
        #region Private Data

        private string inputFilePath;
        private string outputFilePath;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public Properties

        #endregion

        #region Constructor

        public FileConverter(string inputFile, string outputFile)
        {
            const string methodName = "FileConverter";
            logger.Trace(LogHelper.BuildMethodEntryTrace(methodName));

            if (!File.Exists(inputFile))
            {
                throw new ArgumentException(string.Format("The input file '{0}' does not exist.", inputFile), "filePath");
            }

            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            inputFilePath = inputFile;
            outputFilePath = outputFile;
            
            logger.Trace(LogHelper.BuildMethodExitTrace(methodName));
        }

        #endregion

        #region Methods

        public void ConvertFile()
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
                    FileInfo outputFile = new FileInfo(outputFilePath);

                    if (outputFile.Exists)
                    {
                        outputFile.Delete();
                    }

                    using (ExcelPackage package = new ExcelPackage(outputFile))
                    {
                        var readRecords = records.Where(b => b.Read == true).ToList<ShelfariRecord>();
                        CreateReadBooksSheet(readRecords, package);

                        var booksToRead = records.Where(b => b.PlanToRead == true).ToList<ShelfariRecord>();
                        CreateBooksToReadSheet(booksToRead, package);
                        package.Save();
                    }

                    logger.Info("Successfully converted Shelfari CSV to Excel file.");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, string.Format("Error converting file '{0}': {1}.", inputFilePath, ex.Message));
            }

            logger.Trace(LogHelper.BuildMethodExitTrace(methodName));
        }

        private void CreateBooksToReadSheet(List<ShelfariRecord> booksToRead, ExcelPackage package)
        {
            if (!booksToRead.Any())
            {
                return;
            }

            booksToRead = booksToRead.OrderBy(o => o.DateAdded).ToList();
            var table = booksToRead.ConvertToDataTable(false);

            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Books to Read");
            worksheet.Cells["A1"].LoadFromDataTable(table, true);

            using (ExcelRange range = worksheet.Cells["A1:D1"])
            {
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Format date columns 
            worksheet.Column(4).Style.Numberformat.Format = "dd/mm/yyyy";

            // Autofit columns
            // worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Column(1).Width = 40;
            worksheet.Column(1).Style.WrapText = true;
            worksheet.Column(2).Width = 25;
            worksheet.Column(3).AutoFit();
            worksheet.Column(4).AutoFit();
        }

        private void CreateReadBooksSheet(List<ShelfariRecord> readRecords, ExcelPackage package)
        {
            if (!readRecords.Any())
            {
                return;
            }
            
            readRecords = readRecords.OrderByDescending(o => o.DateRead).ToList();
            var table = readRecords.ConvertToDataTable(true);

            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Read Books");
            worksheet.Cells["A1"].LoadFromDataTable(table, true);

            using (ExcelRange range = worksheet.Cells["A1:J1"])
            {
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Format date columns 
            worksheet.Column(4).Style.Numberformat.Format = "dd/mm/yyyy";
            worksheet.Column(5).Style.Numberformat.Format = "dd/mm/yyyy";

            // Autofit columns
            // worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Column(1).Width = 40;
            worksheet.Column(1).Style.WrapText = true;
            worksheet.Column(2).Width = 25;
            worksheet.Column(3).AutoFit();
            worksheet.Column(4).AutoFit();
            worksheet.Column(5).AutoFit();
            worksheet.Column(6).AutoFit();
            worksheet.Column(7).Width = 25;
            worksheet.Column(8).AutoFit();
            worksheet.Column(9).Width = 40;
            worksheet.Column(9).Style.WrapText = true;
        }

        #endregion
    }
}