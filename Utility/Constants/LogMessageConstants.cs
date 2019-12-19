using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utility.Constants
{
    public class LogMessageConstants
    {
        public const string UnknownFileFormat = "Unknown format";
        public static string FileSizeExceeds = "File size greater than: ";
        public static string EmptyFile = "Empty file uploaded!";
        public static string FileUploadFailed = "File upload failed!";
        public static string NoFileSelected = "Please select a file!";
        public static string DeleteFileError = "Error deleting file. Error details:";

        public static string SaveTransactionError = "Error saving transaction. Error details: ";
        public static string GetTransactionErrorByCode = "Error getting transaction by currency code. Error details: ";
        public static string GetTransactionErrorByDateRange = "Error getting transaction by date range. Error details: ";
        public static string GetTransactionErrorByStatus = "Error getting transaction by status. Error details: ";

        public static string SaveTransaction = "Adding transactions.";
        public static string GetTransactionByCode = "Fetching all transactions by currency code.";
        public static string GetTransactionByDateRange = "Fetching all transactions by date range.";
        public static string GetTransactionByStatus = "Fetching all transactions by status.";

        public static string FileUploadBeginning = "File upload beginning.";
        public static string TempFileSaved = "Uploaded file saved.";
        public static string TempFileNotSaved = "Uploaded file not saved.";

        public static string DeletedTemporarilyFile = "Temporarily file deleted.";
        public static string ReceivedTransactionModel = "Received TransactionModel.";
        public const string FileUploadSuccessful = "File uploaded successfully.";

        public const string CsvParseError = "Error parsing CSV. Error details: ";
        public const string NoCsvModel = "No CsvModel found.";
        public const string XmlParseError = "Error parsing XML. Error details: ";
        public const string NoXmlModel = "No XmlModel found.";

        public const string ErrorProcessingFile = "No TransactionModel found. Error occurred during file processing!";
    }
}
