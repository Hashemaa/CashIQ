using AutoMapper;
using CashIQ.Dtos;
using CashIQ.Models;

namespace CashIQ.Profiles
{
	public class ExpensesProfile : Profile
	{
		public ExpensesProfile()
		{
			CreateMap<Expense, ExpenseReadDto>();
			CreateMap<ExpenseCreateDto, Expense>();
			CreateMap<ExpenseUpdateDto, Expense>();
			CreateMap<Expense, ExpenseUpdateDto>();
		}
	}
}
