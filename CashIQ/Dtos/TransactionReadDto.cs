using CashIQ.Enums;

namespace CashIQ.Dtos
{
	public class TransactionReadDto
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = null;
		public decimal Amount { get; set; } = 0.00M;
		public Frequency Frequency { get; set; } = Frequency.Monthly;
	}
}
