using System.Collections.Generic;
using WebApp.EntityModels;

namespace WebApp.Models
{
    public class TransactionModel
    {
        public IList<Transaction> Transcations { get; set; }
        public IList<string> Errors { get; set; }
    }
}
