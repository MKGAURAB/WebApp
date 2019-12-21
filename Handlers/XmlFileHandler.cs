using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using WebApp.EntityModels;
using WebApp.Models;
using WebApp.Utility.Constants;
using WebApp.Utility.Helpers;

namespace WebApp.Handlers
{
    public class XmlFileHandler : IFileHandler
    {
        public TransactionModel GetTranscations(string path, string uploadedFileName)
        {
            var xmlModels = Parse(path);
            if (!xmlModels.Any())
            {
               Log.Information(LogMessageConstants.NoXmlModel);
               return null;
            }

            var transcations = new List<Transaction>();
            var errors       = GetErrors(xmlModels, uploadedFileName);

            if (!errors.Any())
            {
                foreach (var xmlModel in xmlModels)
                {
                    transcations.Add(xmlModel.ToTransaction());
                }
            }

            TransactionModel transactionModel = new TransactionModel { Transcations = transcations, Errors = errors };
            return transactionModel;
        }

        private static IList<string> GetErrors(IList<XmlModel> xmlModels, string uploadedFileName)
        {
            var errors = new List<string>();
            for (var i = 0; i < xmlModels.Count; i++)
            {
                var error = xmlModels[i].IsValid(i + 1, uploadedFileName);
                if (!string.IsNullOrWhiteSpace(error))
                {
                    errors.Add(error);
                }
            }
            return errors;
        }

        private static IList<XmlModel> Parse(string path)
        {
            List<XmlModel> xmlModels = null;
            try
            {
                var serializer = new XmlSerializer(typeof(XmlRootModel));

                using (var sr = new StreamReader(path))
                {
                    if (serializer.Deserialize(sr) is XmlRootModel result)
                    {
                        xmlModels = result.XmlModels;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Information(LogMessageConstants.XmlParseError + e.Message + e.InnerException);
            }
            return xmlModels;
        }
    }
}
