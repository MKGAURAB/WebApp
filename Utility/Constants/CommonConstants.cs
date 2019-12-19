using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utility.Constants
{
    public class CommonConstants
    {
        public const string File = "file";
        public const string Csv  = ".csv";
        public const string Xml  = ".xml";

        public const int BytesFor1Mb = 1048576;

        public const string TransactionId        = "TransactionId";
        public const string TransactionDate      = "TransactionDate";
        public const string PaymentDetails       = "PaymentDetails";
        public const string Amount               = "Amount";
        public const string CurrencyCode         = "CurrencyCode";
        public const string Status               = "Status";
        public const string InvalidItems         = "Found Invalid Items: ";
        public const string DateTimeFormatForCsv = "dd/MM/yyyy HH:mm:ss";
        public const string StopwatchKey         = "DebugLoggingStopWatch";
    }
}
