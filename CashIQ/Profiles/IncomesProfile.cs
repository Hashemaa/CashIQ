using AutoMapper;
using CashIQ.Dtos;
using CashIQ.Models;

namespace CashIQ.Profiles
{
	public class IncomesProfile : Profile
	{
		public IncomesProfile()
		{
			CreateMap<Income, IncomeReadDto>();
			CreateMap<IncomeCreateDto, Income>();
			CreateMap<IncomeUpdateDto, Income>();
			CreateMap<Income, IncomeUpdateDto>();
		}
	}
}
