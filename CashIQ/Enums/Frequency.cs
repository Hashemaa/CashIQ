using System.ComponentModel.DataAnnotations;

namespace CashIQ.Enums
{
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
