using CashIQ.Enums;

namespace CashIQ.Models
{
	public class IncomeViewModel
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = null;
		public decimal Amount { get; set; } = 0.00M;
		public decimal TotalAmount { get; set; } = 0.00M;
		public Frequency Frequency { get; set; } = Frequency.Monthly;
	}
}
