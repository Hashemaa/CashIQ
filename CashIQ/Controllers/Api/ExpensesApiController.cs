using CashIQ.Data;
using CashIQ.Dtos;
using CashIQ.Helpers;
using CashIQ.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CashIQ.Controllers.Api
{
	[Route("api/expenses")]
	[ApiController]
	public class ExpensesApiController(ITransactionRepo transactionRepo, IMapper mapper) : ControllerBase
	{
		private readonly ITransactionRepo _transactionRepo = transactionRepo;
		private readonly IMapper _mapper = mapper;

		//GET api/expenses
		[HttpGet]
		public ActionResult<IEnumerable<ExpenseReadDto>> GetAllExpenses()
		{
			var expensesFromRepo = _transactionRepo.GetAllTransactions<Expense>();

			if (expensesFromRepo != null)
			{
				return Ok(_mapper.Map(expensesFromRepo));
			}

			return NotFound(expensesFromRepo);
		}

		//GET api/expenses/{id}
		[HttpGet("{id}", Name = "GetExpenseById")]
		public ActionResult<ExpenseReadDto?> GetExpenseById(Guid id)
		{
			var expenseFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expenseFromRepo != null)
			{
				return Ok(_mapper.Map(expenseFromRepo));
			}

			return NotFound(expenseFromRepo);
		}

		//POST api/expenses
		[HttpPost]
		public ActionResult<ExpenseReadDto> CreateExpense(ExpenseCreateDto expenseCreateDto)
		{
			var expense = _mapper.Map(expenseCreateDto);
			_transactionRepo.CreateTransaction<Expense>(expense);
			_transactionRepo.SaveChanges();

			var expenseReadDto = _mapper.Map(expense);

			return CreatedAtRoute(nameof(GetExpenseById), new { Id = expenseReadDto.Id }, expenseReadDto);
		}

		//PUT api/expenses/{id}
		[HttpPut("{id}")]
		public ActionResult<ExpenseReadDto> UpdateExpense(Guid id, ExpenseUpdateDto expenseUpdateDto)
		{
			var expenseFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expenseFromRepo == null)
			{
				return NotFound(expenseFromRepo);
			}

			_transactionRepo.UpdateTransaction(_mapper.Map(expenseUpdateDto, expenseFromRepo));
			_transactionRepo.SaveChanges();

			return NoContent();
		}


		//PATCH api/expenses/{id}
		[HttpPatch("{id}")]
		public ActionResult<ExpenseReadDto> PartialExpenseUpdate(Guid id, JsonPatchDocument<ExpenseUpdateDto> patchDoc)
		{
			var expenseFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expenseFromRepo != null)
			{
				var expenseUpdateDto = new ExpenseUpdateDto
				{
					Title = expenseFromRepo.Title,
					Description = expenseFromRepo.Description,
					Amount = expenseFromRepo.Amount,
					Frequency = expenseFromRepo.Frequency,
				};
				patchDoc.ApplyTo(expenseUpdateDto, ModelState);

				if (!TryValidateModel(expenseUpdateDto))
				{
					return ValidationProblem(ModelState);
				}

				_transactionRepo.UpdateTransaction<Expense>(_mapper.Map(expenseUpdateDto, expenseFromRepo));
				_transactionRepo.SaveChanges();

				return NoContent();
			}

			return NotFound(expenseFromRepo);
		}

		//DELETE api/expenses/{id}
		[HttpDelete("{id}")]
		public ActionResult DeleteExpense(Guid id)
		{
			var expenseFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expenseFromRepo != null)
			{
				_transactionRepo.DeleteTransaction<Expense>(expenseFromRepo);
				_transactionRepo.SaveChanges();

				return NoContent();

			}

			return NotFound();
		}
	}
}
