using CashIQ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CashIQ.Controllers
{
	public class ExpenseController(IHttpClientFactory httpClientFactory) : Controller
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CashIQApi");
		private const string apiExpensesRoute = "api/expenses";
		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync(apiExpensesRoute);

			if (!response.IsSuccessStatusCode)
				return View("Error");

			var json = await response.Content.ReadAsStringAsync();
			var expenses = JsonSerializer.Deserialize<IEnumerable<Expense>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});

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
