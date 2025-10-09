using CashIQ.Dtos;
using CashIQ.Models;

namespace CashIQ.Helpers
{
	public class Mapper : IMapper
	{
		public IQueryable<IncomeReadDto>? Map(IQueryable<Income> incomesFromRepo)
		{
			return incomesFromRepo.Select(x => new IncomeReadDto
			{
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
				Amount = x.Amount,
				Frequency = x.Frequency
			});
		}

		public IncomeReadDto Map(Income income)
		{
			return new IncomeReadDto
			{
				Id = income.Id,
				Title = income.Title,
				Description = income.Description,
				Amount = income.Amount,
				Frequency = income.Frequency,
			};
		}

		public Income Map(IncomeCreateDto incomeCreateDto)
		{
			return new Income
			{
				Title = incomeCreateDto.Title,
				Description = incomeCreateDto.Description,
				Amount = incomeCreateDto.Amount,
				Frequency = incomeCreateDto.Frequency,
			};
		}

		public Income Map(IncomeUpdateDto incomeUpdateDto, Income incomeFromRepo)
		{
			incomeFromRepo.Title = incomeUpdateDto.Title;
			incomeFromRepo.Description = incomeUpdateDto.Description;
			incomeFromRepo.Amount = incomeUpdateDto.Amount;
			incomeFromRepo.Frequency = incomeUpdateDto.Frequency;

			return incomeFromRepo;
		}

		public IQueryable<ExpenseReadDto>? Map(IQueryable<Expense> expensesFromRepo)
		{
			return expensesFromRepo.Select(x => new ExpenseReadDto
			{
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
				Amount = x.Amount,
				Frequency = x.Frequency
			});
		}

		public ExpenseReadDto Map(Expense expense)
		{
			return new ExpenseReadDto
			{
				Id = expense.Id,
				Title = expense.Title,
				Description = expense.Description,
				Amount = expense.Amount,
				Frequency = expense.Frequency,
			};
		}

		public Expense Map(ExpenseCreateDto expenseCreateDto)
		{
			return new Expense
			{
				Title = expenseCreateDto.Title,
				Description = expenseCreateDto.Description,
				Amount = expenseCreateDto.Amount,
				Frequency = expenseCreateDto.Frequency,
			};
		}

		public Expense Map(ExpenseUpdateDto expenseUpdateDto, Expense expenseFromRepo)
		{
			expenseFromRepo.Title = expenseUpdateDto.Title;
			expenseFromRepo.Description = expenseUpdateDto.Description;
			expenseFromRepo.Amount = expenseUpdateDto.Amount;
			expenseFromRepo.Frequency = expenseUpdateDto.Frequency;

			return expenseFromRepo;
		}
	}
}
