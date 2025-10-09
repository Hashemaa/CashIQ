using CashIQ.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashIQ.Dtos
{
	public class ExpenseCreateDto : IncomeBaseDto
	{
		[Required]
		public new string Title { get; set; } = string.Empty;
		[Required]
		public new decimal Amount { get; set; } = 0.00M;
		[Required]
		public new Frequency Frequency { get; set; } = Frequency.Monthly;
	}
}
