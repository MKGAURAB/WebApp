using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.EntityModels;
using WebApp.Models;

namespace WebApp.Services
{
    public interface ITransactionService
    {
        bool AddTransactions(IList<Transaction> transactions);
        IQueryable<TransactionResponseDto> GetTransactionsByCurrency(string currencyCode);
        IQueryable<TransactionResponseDto> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);
        IQueryable<TransactionResponseDto> GetTransactionsByStatus(string status);
    }
}
