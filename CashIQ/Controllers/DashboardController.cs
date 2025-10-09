using CashIQ.Models;
using CashIQ.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CashIQ.Controllers
{
    public class DashboardController(ITransactionService transactionService, ILogger<DashboardController> logger) : Controller
    {
        private readonly ITransactionService _transactionService = transactionService;
        private readonly ILogger<DashboardController> _logger = logger;
		public async Task<IActionResult> Index(DashboardViewModel viewModel)
        {
            var incomes = await _transactionService.GetTransactionsViaApiAsync<Income>();
            var expenses = await _transactionService.GetTransactionsViaApiAsync<Expense>();

            if (incomes != null && expenses != null) {
                viewModel.CashFlow = _transactionService.CalculateNetCashFlow(incomes, expenses, viewModel.Frequency);
				return View(viewModel);
			}

            return View("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
