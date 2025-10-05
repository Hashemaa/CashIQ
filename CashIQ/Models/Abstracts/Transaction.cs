using CashIQ.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashIQ.Models.Abstracts
{
	public abstract class Transaction
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = null;
		[Required]
		public decimal Amount { get; set; } = 0.00M;
		[Required]
		public Frequency Frequency { get; set; } = Frequency.Monthly;
		[Required, Display(Name = "Created at")]
		public DateTime Created_at { get; set; } = DateTime.Now;
		[Display(Name = "Updated at")]
		public DateTime? Updated_at { get; set; } = null;
	}
}
