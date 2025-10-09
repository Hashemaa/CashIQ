using CashIQ.Dtos;
using CashIQ.Models;

namespace CashIQ.Helpers
{
	public interface IMapper
	{
		public IQueryable<IncomeReadDto>? Map(IQueryable<Income> incomesFromRepo);
		public IncomeReadDto Map(Income income);
		public Income Map(IncomeCreateDto incomeCreateDto);
		public Income Map(IncomeUpdateDto incomeUpdateDto, Income incomeFromRepo);

		public IQueryable<ExpenseReadDto>? Map(IQueryable<Expense> expensesFromRepo);
		public ExpenseReadDto Map(Expense expense);
		public Expense Map(ExpenseCreateDto expenseCreateDto);
		public Expense Map(ExpenseUpdateDto expenseUpdateDto, Expense expenseFromRepo);
	}
}
