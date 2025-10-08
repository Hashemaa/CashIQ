using AutoMapper;
using CashIQ.Data;
using CashIQ.Dtos;
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
				return Ok(_mapper.Map<IEnumerable<ExpenseReadDto>>(expensesFromRepo));
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
				return Ok(_mapper.Map<ExpenseReadDto>(expenseFromRepo));
			}

			return NotFound(expenseFromRepo);
		}

		//POST api/expenses
		[HttpPost]
		public ActionResult<ExpenseReadDto> CreateExpense(ExpenseCreateDto expenseCreateDto)
		{
			var expense = _mapper.Map<Expense>(expenseCreateDto);
			_transactionRepo.CreateTransaction<Expense>(expense);
			_transactionRepo.SaveChanges();

			var expenseReadDto = _mapper.Map<ExpenseReadDto>(expense);

			return CreatedAtRoute(nameof(GetExpenseById), new { Id = expenseReadDto.Id }, expenseReadDto);
		}

		//PUT api/expenses/{id}
		[HttpPut("{id}")]
		public ActionResult<ExpenseReadDto> UpdateExpense(Guid id, ExpenseUpdateDto expenseUpdateDto)
		{
			var expensesFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expensesFromRepo == null)
			{
				return NotFound(expensesFromRepo);
			}

			_mapper.Map(expenseUpdateDto, expensesFromRepo);
			//_transactionRepo.UpdateTransaction(expensesFromRepo);
			_transactionRepo.SaveChanges();

			return NoContent();
		}


		//PATCH api/expenses/{id}
		[HttpPatch("{id}")]
		public ActionResult<ExpenseReadDto> PartialExpenseUpdate(Guid id, JsonPatchDocument<ExpenseUpdateDto> expenseUpdateDto)
		{
			var expenseFromRepo = _transactionRepo.GetTransactionById<Expense>(id);

			if (expenseFromRepo != null)
			{
				var expenseToPatch = _mapper.Map<ExpenseUpdateDto>(expenseFromRepo);
				expenseUpdateDto.ApplyTo(expenseToPatch, ModelState);

				if (!TryValidateModel(expenseToPatch))
				{
					return ValidationProblem(ModelState);
				}

				_mapper.Map(expenseToPatch, expenseFromRepo);
				//_transactionRepo.UpdateTransaction<Expense>(expenseFromRepo);
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
