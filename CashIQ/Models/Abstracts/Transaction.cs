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
		public double Price { get; set; } = 0.00D;
		[Required]
		public Frequency Frequency { get; set; } = Frequency.Monthly;
		[Required, Display(Name = "Created at")]
		public DateTime Created_at { get; set; } = DateTime.Now;
		[Display(Name = "Updated at")]
		public DateTime? Updated_at { get; set; } = null;
	}

	public enum Frequency
	{
		[Display(Name = "Daily")]
		Daily,
		[Display(Name = "Weekly")]
		Weekly,
		[Display(Name = "Bi-Weekly")]
		BiWeekly,
		[Display(Name = "Monthly")]
		Monthly,
		[Display(Name = "Quarterly")]
		Quarterly,
		[Display(Name = "Semi-Anually")]
		SemiAnually,
		[Display(Name = "Yearly")]
		Yearly
	}
}
