using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.EntityModels;
using WebApp.Models;
using WebApp.Utility.Constants;

namespace WebApp.Utility.Helpers
{
    public static class XmlHelper
    {
        public static string IsValid(this XmlModel xmlModel, int index, string uploadedFileName)
        {
            var invalidItems = new List<string>();
            var paymentDetails = new List<string> { CommonConstants.Amount, CommonConstants.CurrencyCode };

            if (string.IsNullOrWhiteSpace(xmlModel.Id))
            {
                invalidItems.Add(CommonConstants.TransactionId);
            }

            if (string.IsNullOrWhiteSpace(xmlModel.TransactionDate) ||
                !DateTime.TryParse(xmlModel.TransactionDate, out var transactionDate))
            {
                invalidItems.Add(CommonConstants.TransactionDate);
            }

            if (string.IsNullOrWhiteSpace(xmlModel.Status))
            {
                invalidItems.Add(CommonConstants.Status);
            }

            if (xmlModel.PaymentDetails == null)
            {
                invalidItems.AddRange(paymentDetails);
            }
            else if (string.IsNullOrWhiteSpace(xmlModel.PaymentDetails.Amount) ||
                     !decimal.TryParse(xmlModel.PaymentDetails.Amount, out var amount))
            {
                invalidItems.Add(paymentDetails[0]);
            }
            else if (string.IsNullOrWhiteSpace(xmlModel.PaymentDetails.CurrencyCode))
            {
                invalidItems.Add(paymentDetails[1]);
            }

            if (!invalidItems.Any()) return string.Empty;

            var errorMsg = $"In File: {uploadedFileName}, Transaction #{index}, {CommonConstants.InvalidItems}" + string.Join(", ", invalidItems);
            return errorMsg;
        }

        public static Transaction ToTransaction(this XmlModel xmlModel)
        {
            Transaction transactionDto = new Transaction { TransactionId = xmlModel.Id };

            DateTime.TryParse(xmlModel.TransactionDate, out var dateTime);
            transactionDto.TransactionDate = dateTime;
            
            decimal.TryParse(xmlModel.PaymentDetails.Amount, out var amount);
            transactionDto.Amount = amount;

            transactionDto.CurrencyCode = xmlModel.PaymentDetails.CurrencyCode;
            transactionDto.Status       = xmlModel.Status;
            
            return transactionDto;
        }
    }
}
