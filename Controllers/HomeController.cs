using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;
using WebApp.Handlers;
using WebApp.Models;
using WebApp.Services;
using WebApp.Utility.Constants;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ITransactionService TransactionService { get; set; }

        public HomeController(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [MetaDataValidator]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\tempfiles");
            var uploadedFileName = Path.GetFileName(file.FileName);
            var tempFileId = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(rootPath, tempFileId);

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                await file.CopyToAsync(stream);
            }

            TransactionModel transactionModel;
            if (System.IO.File.Exists(path))
            {
                var fileHandler = FileHandlerFactory.GetFileHandler(path);
                transactionModel = fileHandler.GetTranscations(path, uploadedFileName);
            }
            else
            {
                return BadRequest(LogMessageConstants.FileUploadFailed);
            }

            try
            {
                System.IO.File.Delete(path);
                Log.Information(LogMessageConstants.DeleteFileSuccess);
            }
            catch (Exception e)
            {
                Log.Information(LogMessageConstants.DeleteFileError + e.Message);
            }

            if (transactionModel == null)
            {
                throw new Exception(LogMessageConstants.ErrorProcessingFile);
            }

            if (transactionModel.Errors.Any())
            {
                var errorMsg = string.Join("\n", transactionModel.Errors);
                return BadRequest(errorMsg);
            }

            var isSaved = TransactionService.AddTransactions(transactionModel.Transcations);
            if (isSaved)
            {
                return Ok(LogMessageConstants.FileUploadSuccessful);
            }

            return BadRequest(LogMessageConstants.FileUploadFailed);
        }
    }
}
