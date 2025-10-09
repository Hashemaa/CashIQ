using CashIQ.Data;
using CashIQ.Dtos;
using CashIQ.Helpers;
using CashIQ.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
					return Ok(_mapper.Map(incomesFromRepo));
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
				return Ok(_mapper.Map(incomeFromRepo));
			}

			return NotFound(incomeFromRepo);
		}

		//POST api/incomes
		[HttpPost]
		public ActionResult<IncomeReadDto> CreateIncome(IncomeCreateDto incomeCreateDto)
		{
			var income = _mapper.Map(incomeCreateDto);
			_transactionRepo.CreateTransaction<Income>(income);
			_transactionRepo.SaveChanges();

			var incomeReadDto = _mapper.Map(income);

			return CreatedAtRoute(nameof(GetIncomeById), new { Id = incomeReadDto.Id }, incomeReadDto);
		}

		//PUT api/incomes/{id}
		[HttpPut("{id}")]
		public ActionResult<IncomeReadDto> UpdateIncome(Guid id, IncomeUpdateDto incomeUpdateDto)
		{
			var incomeFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomeFromRepo == null)
			{
				return NotFound(incomeFromRepo);
			}
			
			_transactionRepo.UpdateTransaction(_mapper.Map(incomeUpdateDto, incomeFromRepo));
			_transactionRepo.SaveChanges();

			return NoContent();
		}
		

		//PATCH api/incomes/{id}
		[HttpPatch("{id}")]
		public ActionResult<IncomeReadDto> PartialIncomeUpdate(Guid id, JsonPatchDocument<IncomeUpdateDto> patchDoc)
		{
			var incomeFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomeFromRepo != null)
			{
				var incomeUpdateDto = new IncomeUpdateDto
				{
					Title = incomeFromRepo.Title,
					Description = incomeFromRepo.Description,
					Amount = incomeFromRepo.Amount,
					Frequency = incomeFromRepo.Frequency,
				};
				patchDoc.ApplyTo(incomeUpdateDto, ModelState);

				if (!TryValidateModel(incomeUpdateDto))
				{
					return ValidationProblem(ModelState);
				}
				
				_transactionRepo.UpdateTransaction<Income>(_mapper.Map(incomeUpdateDto, incomeFromRepo));
				_transactionRepo.SaveChanges();

				return NoContent();
			}

				return NotFound(incomeFromRepo);
		}

		//DELETE api/incomes/{id}
		[HttpDelete("{id}")]
		public ActionResult DeleteIncome(Guid id)
		{
			var incomeFromRepo = _transactionRepo.GetTransactionById<Income>(id);

			if (incomeFromRepo != null)
			{
				_transactionRepo.DeleteTransaction<Income>(incomeFromRepo);
				_transactionRepo.SaveChanges();

				return NoContent();
				
			}

			return NotFound();
		}
	}
}
