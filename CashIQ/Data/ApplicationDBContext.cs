using CashIQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CashIQ.Data
{
	public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
	{
		public DbSet<Income> Incomes { get; set; }
		public DbSet<Expense> Expenses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
