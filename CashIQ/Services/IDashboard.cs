using CashIQ.Enums;

namespace CashIQ.Services
{
	public interface IDashboard
	{
		public decimal CalculateDifference(decimal a, decimal b);
		public decimal CalculateDifference(decimal a, decimal b, Frequency frequency);
	}
}
