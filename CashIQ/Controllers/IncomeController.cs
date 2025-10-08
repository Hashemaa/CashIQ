using CashIQ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CashIQ.Controllers
{
	public class IncomeController(IHttpClientFactory httpClientFactory) : Controller
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CashIQApi");
		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync("api/incomes");

			if (!response.IsSuccessStatusCode)
				return View("Error");

			var json = await response.Content.ReadAsStringAsync();
			var incomes = JsonSerializer.Deserialize<IEnumerable<Income>>(json, new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			});

			return View(incomes);
		}
	}
}
