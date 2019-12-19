using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.EntityModels;
using WebApp.Models;
using WebApp.Utility.Constants;

namespace WebApp.Services
{
    public class TransactionService : ITransactionService
    {
        public TransactionDbContext DbContext { get; set; }

        public TransactionService(TransactionDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool AddTransactions(IList<Transaction> transactions)
        {
            try
            {
                Console.WriteLine(LogMessageConstants.SaveTransaction);
                DbContext.Transaction.AddRange(transactions);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var error = LogMessageConstants.SaveTransactionError + e.Message + e.InnerException;
                Console.WriteLine(error);
                return false;
            }
        }

        public IQueryable<TransactionResponseDto> GetTransactionsByCurrency(string currencyCode)
        {
            try
            {
               Console.WriteLine(LogMessageConstants.GetTransactionByCode);

                var ctx      = DbContext;
                var response = from txn in ctx.Transaction
                               join txns in ctx.TransactionStatus
                               on txn.Status equals txns.Status
                               where string.Equals(txn.CurrencyCode, currencyCode, StringComparison.CurrentCultureIgnoreCase)
                               select new TransactionResponseDto
                               {
                                   Id      = txn.TransactionId,
                                   Payment = string.Concat(txn.Amount, " ", txn.CurrencyCode),
                                   Status  = txns.Symbol
                               };
                return response;
            }
            catch (Exception e)
            {
                var error = LogMessageConstants.GetTransactionErrorByCode + e.Message + e.InnerException;
                throw new Exception(error);
            }
        }

        public IQueryable<TransactionResponseDto> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                Console.WriteLine(LogMessageConstants.GetTransactionByDateRange);

                var ctx      = DbContext;
                var response = from txn in ctx.Transaction
                               join txns in ctx.TransactionStatus
                               on txn.Status equals txns.Status
                               where txn.TransactionDate >= startDate && txn.TransactionDate <= endDate
                               select new TransactionResponseDto
                               {
                                   Id      = txn.TransactionId,
                                   Payment = string.Concat(txn.Amount, " ", txn.CurrencyCode),
                                   Status  = txns.Symbol
                               };
                return response;
            }
            catch (Exception e)
            {
                var error = LogMessageConstants.GetTransactionErrorByDateRange + e.Message + e.InnerException;
                throw new Exception(error);
            }
        }

        public IQueryable<TransactionResponseDto> GetTransactionsByStatus(string status)
        {
            try
            {
                Console.WriteLine(LogMessageConstants.GetTransactionByStatus);

                var ctx      = DbContext;
                var response = from txn in ctx.Transaction
                               join txns in ctx.TransactionStatus
                               on txn.Status equals txns.Status
                               where string.Equals(txn.Status, status, StringComparison.CurrentCultureIgnoreCase)
                               select new TransactionResponseDto
                               {
                                   Id      = txn.TransactionId,
                                   Payment = string.Concat(txn.Amount, " ", txn.CurrencyCode),
                                   Status  = txns.Symbol
                               };
                return response;
            }
            catch (Exception e)
            {
                var error = LogMessageConstants.GetTransactionErrorByStatus + e.Message + e.InnerException;
                throw new Exception(error);
            }
        }
    }
}
