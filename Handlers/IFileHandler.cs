using WebApp.Models;

namespace WebApp.Handlers
{
    public interface IFileHandler
    {
        TransactionModel GetTranscations(string path, string uploadedFileName);
    }
}
