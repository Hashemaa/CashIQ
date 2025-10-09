using CashIQ.Models;
using CashIQ.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CashIQ.Controllers
{
	public class ExpenseController(IHttpClientFactory httpClientFactory, ITransactionService transactionService) : Controller
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CashIQApi");
		private readonly ITransactionService _transactionService = transactionService;
		private const string apiExpensesRoute = "api/expenses";

		public async Task<IActionResult> Index(ExpenseViewModel viewModel)
		{
			var expenses = await _transactionService.GetTransactionsViaApiAsync<Expense>();
			if (expenses != null)
			{
				foreach (var item in expenses)
				{
					viewModel.TotalAmount += _transactionService.ConvertAmount(item.Amount, item.Frequency, viewModel.Frequency);
				}
				viewModel.TotalAmount = Math.Round(viewModel.TotalAmount, 2);
				ViewData["viewModel"] = viewModel;
			}
			return View(expenses);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Expense obj)
		{
			if (ModelState.IsValid)
			{
				var json = JsonSerializer.Serialize<Expense>(obj);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync(apiExpensesRoute, content);

				if (!response.IsSuccessStatusCode)
					return View("Error");

				return RedirectToAction("Index");
			}

			return View(obj);
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var response = await _httpClient.GetAsync($"{apiExpensesRoute}/{id}");

			if (!response.IsSuccessStatusCode)
				return View("Error");

			var json = await response.Content.ReadAsStringAsync();
			var expenseToEdit = JsonSerializer.Deserialize<Expense>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});

			if (expenseToEdit != null)
			{

				return View(expenseToEdit);
			}

			return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Expense obj)
		{
			if (ModelState.IsValid)
			{
				obj.Updated_at = DateTime.Now;
				var json = JsonSerializer.Serialize<Expense>(obj);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PutAsync($"{apiExpensesRoute}/{obj.Id}", content);

				if (!response.IsSuccessStatusCode)
					return View("Error");

				return RedirectToAction("Index");
			}

			return View(obj);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"{apiExpensesRoute}/{id}");

			if (!response.IsSuccessStatusCode)
				return View("Error");

			return RedirectToAction("Index");
		}
	}
}
