using CashIQ.Enums;

namespace CashIQ.Services
{
	public class Dashboard : IDashboard
	{
		public decimal CalculateDifference(decimal a, decimal b, bool isAbsolute = false)
		{
			decimal result = a - b;
			if (isAbsolute)
				result = Math.Abs(result);

			return Math.Round(result, 2);
		}

		public decimal ConvertAmount(decimal amount, Frequency from, Frequency to)
		{
			const decimal daysPerYear = 365m;
			const decimal weeksPerYear = 52m;
			const decimal monthsPerYear = 12m;
			const decimal quartersPerYear = 4m;
			const decimal semiAnnualsPerYear = 2m;
			const decimal yearsPerYear = 1m;

			decimal annualAmount = from switch
			{
				Frequency.Daily => amount * daysPerYear,
				Frequency.Weekly => amount * weeksPerYear,
				Frequency.BiWeekly => amount * (weeksPerYear / 2),
				Frequency.Monthly => amount * monthsPerYear,
				Frequency.Quarterly => amount * quartersPerYear,
				Frequency.SemiAnnually => amount * semiAnnualsPerYear,
				Frequency.Yearly => amount * yearsPerYear,
				_ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
			};

			decimal converted = to switch
			{
				Frequency.Daily => annualAmount / daysPerYear,
				Frequency.Weekly => annualAmount / weeksPerYear,
				Frequency.BiWeekly => annualAmount / (weeksPerYear / 2),
				Frequency.Monthly => annualAmount / monthsPerYear,
				Frequency.Quarterly => annualAmount / quartersPerYear,
				Frequency.SemiAnnually => annualAmount / semiAnnualsPerYear,
				Frequency.Yearly => annualAmount / yearsPerYear,
				_ => throw new ArgumentOutOfRangeException(nameof(to), to, null)
			};

			return Math.Round(converted, 2);
		}
	}
}
