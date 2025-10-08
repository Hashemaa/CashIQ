using AutoMapper;
using CashIQ.Data;
using CashIQ.Dtos;
using CashIQ.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CashIQ.Controllers.Api
{
	[Route("api/incomes")]
	[ApiController]
	public class IncomesApiController(ITransactionRepo transactionRepo, IMapper mapper) : ControllerBase
	{
		private readonly ITransactionRepo _transactionRepo = transactionRepo;
		private readonly IMapper _mapper = mapper;

		//GET api/incomes
		[HttpGet]
		public ActionResult<IEnumerable<IncomeReadDto>> GetAllIncomes()
		{
			var incomesFromRepo = _transactionRepo.GetAllTransactions<Income>();

			if (incomesFromRepo != null)
			{
				return Ok(_mapper.Map<IEnumerable<IncomeReadDto>>(incomesFromRepo));
			}

			return NotFound(incomesFromRepo);
		}

		//GET api/incomes/{id}
		[HttpGet("{id}", Name = "GetIncomeById")]
		public ActionResult<IncomeReadDto?> GetIncomeById(Guid id)
		{
			var incomeFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomeFromRepo != null)
			{
				return Ok(_mapper.Map<IncomeReadDto>(incomeFromRepo));
			}

			return NotFound(incomeFromRepo);
		}

		//POST api/incomes
		[HttpPost]
		public ActionResult<IncomeReadDto> CreateIncome(IncomeCreateDto incomeCreateDto)
		{
			var income = _mapper.Map<Income>(incomeCreateDto);
			_transactionRepo.CreateTransaction<Income>(income);
			_transactionRepo.SaveChanges();

			var incomeReadDto = _mapper.Map<IncomeReadDto>(income);

			return CreatedAtRoute(nameof(GetIncomeById), new { Id = income.Id }, incomeReadDto); //incomeReadDto.Id <-- future consideration
		}

		//PUT api/incomes/{id}
		[HttpPut("{id}")]
		public ActionResult<IncomeReadDto> UpdateIncome(Guid id, IncomeUpdateDto incomeUpdateDto)
		{
			var incomesFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomesFromRepo == null)
			{
				return NotFound(incomesFromRepo);
			}

			_mapper.Map(incomeUpdateDto, incomesFromRepo);
			//_transactionRepo.UpdateTransaction(incomesFromRepo);
			_transactionRepo.SaveChanges();

			return NoContent();
		}
		

		//PATCH api/incomes/{id}
		[HttpPatch("{id}")]
		public ActionResult<IncomeReadDto> PartialIncomeUpdate(Guid id, JsonPatchDocument<IncomeUpdateDto> incomeUpdateDto)
		{
			var incomeFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomeFromRepo != null)
			{
				var incomeToPatch = _mapper.Map<IncomeUpdateDto>(incomeFromRepo);
				incomeUpdateDto.ApplyTo(incomeToPatch, ModelState);

				if (!TryValidateModel(incomeToPatch))
				{
					return ValidationProblem(ModelState);
				}

				_mapper.Map(incomeToPatch, incomeFromRepo);
				//_transactionRepo.UpdateTransaction<Income>(incomeFromRepo);
				_transactionRepo.SaveChanges();
				return NoContent();
			}
			else
			{
				return NotFound(incomeFromRepo);
			}
		}
	}
}
