using CashIQ.Enums;
using CashIQ.Models;
using CashIQ.Models.Abstracts;
using System.Text.Json;

namespace CashIQ.Services
{
	public class TransactionService(IHttpClientFactory httpClientFactory) : ITransactionService
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CashIQApi");
		public async Task<IEnumerable<T>?> GetTransactionsViaApiAsync<T>() where T : Transaction
		{
			string apiRoute = $"api/{typeof(T).Name.ToLower()}s";
			var response = await _httpClient.GetAsync(apiRoute);
			response.EnsureSuccessStatusCode();
			var json = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});
		}

		public decimal CalculateDifference(decimal a, decimal b, bool isAbsolute = false)
		{
			decimal result = a - b;
			if (isAbsolute)
				result = Math.Abs(result);

			return Math.Round(result, 2);
		}

		public decimal ConvertAmount(decimal amount, Frequency from, Frequency to)
		{
			const decimal daysPerYear = 365m;
			const decimal weeksPerYear = 52m;
			const decimal monthsPerYear = 12m;
			const decimal quartersPerYear = 4m;
			const decimal semiAnnualsPerYear = 2m;
			const decimal yearsPerYear = 1m;

			decimal annualAmount = from switch
			{
				Frequency.Daily => amount * daysPerYear,
				Frequency.Weekly => amount * weeksPerYear,
				Frequency.BiWeekly => amount * (weeksPerYear / 2),
				Frequency.Monthly => amount * monthsPerYear,
				Frequency.Quarterly => amount * quartersPerYear,
				Frequency.SemiAnnually => amount * semiAnnualsPerYear,
				Frequency.Yearly => amount * yearsPerYear,
				_ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
			};

			decimal converted = to switch
			{
				Frequency.Daily => annualAmount / daysPerYear,
				Frequency.Weekly => annualAmount / weeksPerYear,
				Frequency.BiWeekly => annualAmount / (weeksPerYear / 2),
				Frequency.Monthly => annualAmount / monthsPerYear,
				Frequency.Quarterly => annualAmount / quartersPerYear,
				Frequency.SemiAnnually => annualAmount / semiAnnualsPerYear,
				Frequency.Yearly => annualAmount / yearsPerYear,
				_ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
			};

			return converted;
		}

		public decimal CalculateNetCashFlow(IEnumerable<Income> incomes, IEnumerable<Expense> expenses, bool isAbsolute = false)
		{
			return isAbsolute ? Math.Abs(Math.Round(incomes.Sum(x => x.Amount) - expenses.Sum(x => x.Amount), 2)) : Math.Round(incomes.Sum(x => x.Amount) - expenses.Sum(x => x.Amount), 2);
		}

		public decimal CalculateNetCashFlow(IEnumerable<Income> incomes, IEnumerable<Expense> expenses, Frequency to, bool isAbsolute = false)
		{
			decimal totalIncome = 0.00M;
			foreach (var income in incomes)
			{
				totalIncome += ConvertAmount(income.Amount, income.Frequency, to);
			}

			decimal totalExpense = 0.00M;
			foreach (var expense in expenses)
			{
				totalExpense += ConvertAmount(expense.Amount, expense.Frequency, to);
			}

			return isAbsolute ? Math.Abs(Math.Round(totalIncome - totalExpense, 2)) : Math.Round(totalIncome - totalExpense, 2);
		}
	}
}
