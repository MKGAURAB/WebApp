using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApp.Models;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TransactionController : Controller
    {
        public ITransactionService TransactionService { get; set; }
        public TransactionController(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(TransactionResponseDto), Description = "Get Transactions by CurrencyCode")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null)]
        public IEnumerable<TransactionResponseDto> GetByCurrency([FromQuery] string currencyCode)
        {
            var response = TransactionService.GetTransactionsByCurrency(currencyCode);
            return response;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(TransactionResponseDto), Description = "Get Transactions by Date Range")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null)]
        public IEnumerable<TransactionResponseDto> GetByDateRange([FromQuery] string startDate, [FromQuery] string endDate)
        {
            var sd = Convert.ToDateTime(startDate);
            var ed = Convert.ToDateTime(endDate);

            var response = TransactionService.GetTransactionsByDateRange(sd, ed);
            return response;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(TransactionResponseDto), Description = "Get Transactions by Status")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null)]
        public IEnumerable<TransactionResponseDto> GetByStatus(string status)
        {
            var response = TransactionService.GetTransactionsByStatus(status);
            return response;
        }

    }
}
