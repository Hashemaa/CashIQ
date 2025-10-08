using CashIQ.Models.Abstracts;

namespace CashIQ.Data
{
	public class TransactionRepo(ApplicationDBContext context) : ITransactionRepo
	{
		private readonly ApplicationDBContext _context = context;

		public void CreateTransaction<T>(T transactionEntity) where T : Transaction
		{
			ArgumentNullException.ThrowIfNull(transactionEntity);

			_context.Add(transactionEntity);
		}

		public void DeleteTransaction<T>(T transactionEntity) where T : Transaction
		{
			ArgumentNullException.ThrowIfNull(transactionEntity);

			_context.Remove(transactionEntity);
		}

		public IQueryable<T> GetAllTransactions<T>() where T : Transaction
		{
			return _context.Set<T>();
		}

		public T? GetTransactionById<T>(Guid id) where T : Transaction
		{
			return _context.Find<T>(id);
		}

		public bool SaveChanges()
		{
			return _context.SaveChanges() > 0;
		}

		public void UpdateTransaction<T>(T transactionEntity) where T : Transaction
		{
			ArgumentNullException.ThrowIfNull(transactionEntity);

			_context.Update(transactionEntity);
		}
	}
}
