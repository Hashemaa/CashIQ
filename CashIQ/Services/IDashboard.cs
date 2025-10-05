using CashIQ.Enums;

namespace CashIQ.Services
{
	public interface IDashboard
	{
		public decimal CalculateDifference(decimal a, decimal b, bool isAbsolute = false);
		public decimal ConvertAmount(decimal amount, Frequency from, Frequency to);
	}
}
