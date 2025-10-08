using CashIQ.Models;
using CashIQ.Models.Abstracts;

namespace CashIQ.Data
{
	public interface ITransactionRepo
	{
		public bool SaveChanges();
		public void CreateTransaction<T>(T transactionEntity) where T : Transaction;
		public IQueryable<T> GetAllTransactions<T>() where T : Transaction;
		public void UpdateTransaction<T>(T transactionEntity) where T : Transaction;
		public void DeleteTransaction<T>(T transactionEntity) where T : Transaction;
		public T? GetTransactionById<T>(Guid id) where T : Transaction;
	}
}
