using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CsvModel
    {
        public string TransactionIdentificator { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
