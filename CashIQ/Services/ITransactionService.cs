using CashIQ.Enums;
using CashIQ.Models;
using CashIQ.Models.Abstracts;

namespace CashIQ.Services
{
	public interface ITransactionService
	{
		public Task<IEnumerable<T>?> GetTransactionsViaApiAsync<T>() where T : Transaction;
		public decimal CalculateDifference(decimal a, decimal b, bool isAbsolute = false);
		public decimal ConvertAmount(decimal amount, Frequency from, Frequency to);
		public decimal CalculateNetCashFlow(IEnumerable<Income> incomes, IEnumerable<Expense> expenses, bool isAbsolute = false);
		public decimal CalculateNetCashFlow(IEnumerable<Income> incomes, IEnumerable<Expense> expenses, Frequency to, bool isAbsolute = false);
	}
}
