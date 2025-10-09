using CashIQ.Enums;

namespace CashIQ.Models
{
	public class DashboardViewModel
	{
		public decimal CashFlow { get; set; } = 0.00M;
		public Frequency Frequency { get; set; } = Frequency.Monthly;
	}
}
