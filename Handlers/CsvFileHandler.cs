using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApp.EntityModels;
using WebApp.Models;
using WebApp.Utility.Constants;
using WebApp.Utility.Helpers;

namespace WebApp.Handlers
{
    public class CsvFileHandler : IFileHandler
    {
        public TransactionModel GetTranscations(string path, string uploadedFileName)
        {
            var csvModels = Parse(path);
            if (!csvModels.Any())
            {
                Console.WriteLine(LogMessageConstants.NoCsvModel);
                return null;
            }

            List<Transaction> transcations = new List<Transaction>();
            IList<string> errors            = GetErrors(csvModels, uploadedFileName);

            if (!errors.Any())
            {
                foreach (CsvModel csvModel in csvModels)
                {
                    transcations.Add(csvModel.ToTransaction());
                }
            }

            var transactionModel = new TransactionModel { Transcations = transcations, Errors = errors };
            return transactionModel;
        }

        private static IList<string> GetErrors(IList<CsvModel> csvModels, string uploadedFileName)
        {
            List<string> errors = new List<string>();
            for (var i = 0; i < csvModels.Count; i++)
            {
                var error = csvModels[i].IsValid(i + 1, uploadedFileName);
                if (!string.IsNullOrWhiteSpace(error))
                {
                    errors.Add(error);
                }
            }
            return errors;
        }

        private static IList<CsvModel> Parse(string path)
        {
            char[] splitChar = new[] { ',', '"', '“', '”', '\n', '\r', '\t' };
            List<CsvModel> csvModels = new List<CsvModel>();

            try
            {
                string[] lines = File.ReadAllLines(path, Encoding.Default);

                foreach (string line in lines)
                {
                    string[] items         = line.Split(splitChar);
                    List<string> lineItems = new List<string>(items);
                    int i                  = 1;
                    string amount          = lineItems[i];


                    lineItems.RemoveAll(string.IsNullOrWhiteSpace);

                    if (lineItems.Count > 5)
                    {
                        amount           = string.Empty;
                        int amountLength = lineItems.Count - 5;

                        for (; i <= amountLength + 1; i++)
                        {
                            amount += lineItems[i];
                        }
                    }
                    else
                    {
                        i++;
                    }

                    var csvModel = new CsvModel
                    {
                        TransactionIdentificator = lineItems[0].Trim(),
                        Amount                   = amount.Trim(),
                        CurrencyCode             = lineItems[i++].Trim(),
                        TransactionDate          = lineItems[i++].Trim(),
                        Status                   = lineItems[i].Trim()
                    };
                    csvModels.Add(csvModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(LogMessageConstants.CsvParseError + e.Message + e.InnerException);
            }
            return csvModels;
        }
    }
}
