using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebApp.EntityModels;
using WebApp.Models;
using WebApp.Utility.Constants;

namespace WebApp.Utility.Helpers
{
    public static class CsvHelper
    {
        public static string IsValid(this CsvModel csvModel, int index, string uploadedFileName)
        {
            List<string> invalidItems = new List<string>();

            if (string.IsNullOrWhiteSpace(csvModel.TransactionIdentificator))
            {
                invalidItems.Add(CommonConstants.TransactionId);
            }

            if (string.IsNullOrWhiteSpace(csvModel.Status))
            {
                invalidItems.Add(CommonConstants.Status);
            }

            if (string.IsNullOrWhiteSpace(csvModel.Amount) ||
                !decimal.TryParse(csvModel.Amount, out var amount))
            {
                invalidItems.Add(CommonConstants.Amount);
            }

            if (string.IsNullOrWhiteSpace(csvModel.CurrencyCode))
            {
                invalidItems.Add(CommonConstants.CurrencyCode);
            }

            if (!string.IsNullOrWhiteSpace(csvModel.TransactionDate))
            {
                try
                {
                    var dt = DateTime.ParseExact(csvModel.TransactionDate, CommonConstants.DateTimeFormatForCsv, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    invalidItems.Add(CommonConstants.TransactionDate);
                }
            }
            else if (string.IsNullOrWhiteSpace(csvModel.TransactionDate))
            {
                invalidItems.Add(CommonConstants.TransactionDate);
            }

            if (!invalidItems.Any()) return string.Empty;

            var msg = $"In File: {uploadedFileName}, Transaction #{index}, {CommonConstants.InvalidItems}" + string.Join(", ", invalidItems);
            return msg;
        }

        public static Transaction ToTransaction(this CsvModel csvModel)
        {
            Transaction transactionDto = new Transaction { TransactionId = csvModel.TransactionIdentificator };
            
            DateTime dateTime              = DateTime.ParseExact(csvModel.TransactionDate, CommonConstants.DateTimeFormatForCsv, CultureInfo.InvariantCulture);
            transactionDto.TransactionDate = dateTime;
            
            decimal.TryParse(csvModel.Amount, out var amount);
            transactionDto.Amount = amount;
            
            transactionDto.CurrencyCode = csvModel.CurrencyCode;
            transactionDto.Status       = csvModel.Status;
            
            return transactionDto;
        }
    }
}
