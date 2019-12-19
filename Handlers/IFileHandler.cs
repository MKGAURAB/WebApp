using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Handlers
{
    public interface IFileHandler
    {
        TransactionModel GetTranscations(string path, string uploadedFileName);
    }
}
