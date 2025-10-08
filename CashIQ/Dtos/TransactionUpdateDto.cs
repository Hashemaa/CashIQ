using CashIQ.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashIQ.Dtos
{
	public class TransactionUpdateDto
	{
		[Required]
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = null;
		[Required]
		public decimal Amount { get; set; } = 0.00M;
		[Required]
		public Frequency Frequency { get; set; } = Frequency.Monthly;
	}
}
