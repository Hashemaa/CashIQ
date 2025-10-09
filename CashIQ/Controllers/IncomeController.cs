using CashIQ.Models;
using CashIQ.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CashIQ.Controllers
{
	public class IncomeController(IHttpClientFactory httpClientFactory, ITransactionService transactionService) : Controller
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CashIQApi");
		private readonly ITransactionService _transactionService = transactionService;
		private const string apiIncomesRoute = "api/incomes";

		public async Task<IActionResult> Index(IncomeViewModel viewModel)
		{
			var incomes = await _transactionService.GetTransactionsViaApiAsync<Income>();
			if (incomes != null)
			{
				foreach (var item in incomes)
				{
					viewModel.TotalAmount += _transactionService.ConvertAmount(item.Amount, item.Frequency, viewModel.Frequency);
				}
				viewModel.TotalAmount = Math.Round(viewModel.TotalAmount, 2);
				ViewData["viewModel"] = viewModel;
			}
			return View(incomes);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Income obj)
		{
			if (ModelState.IsValid)
			{
				var json = JsonSerializer.Serialize<Income>(obj);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync(apiIncomesRoute, content);

				if (!response.IsSuccessStatusCode)
					return View("Error");

				return RedirectToAction("Index");
			}

			return View(obj);
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var response = await _httpClient.GetAsync($"{apiIncomesRoute}/{id}");

			if (!response.IsSuccessStatusCode)
				return View("Error");

			var json = await response.Content.ReadAsStringAsync();
			var incomeToEdit = JsonSerializer.Deserialize<Income>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});

			if (incomeToEdit != null)
			{

				return View(incomeToEdit);
			}

			return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Income obj)
		{
			if (ModelState.IsValid)
			{
				obj.Updated_at = DateTime.Now;
				var json = JsonSerializer.Serialize<Income>(obj);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PutAsync($"{apiIncomesRoute}/{obj.Id}", content);

				if (!response.IsSuccessStatusCode)
					return View("Error");

				return RedirectToAction("Index");
			}

			return View(obj);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"{apiIncomesRoute}/{id}");

			if (!response.IsSuccessStatusCode)
				return View("Error");

			return RedirectToAction("Index");
		}
	}
}
