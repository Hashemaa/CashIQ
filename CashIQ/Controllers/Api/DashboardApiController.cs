using CashIQ.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashIQ.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardApiController(ITransactionRepo transactionRepo) : ControllerBase
	{
		private readonly ITransactionRepo _transactionRepo = transactionRepo;
	}
}
