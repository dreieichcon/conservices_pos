using Innkeep.Db.Server.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Context;

public class InnkeepTransactionContext(DbContextOptions<InnkeepTransactionContext> options) : DbContext(options)
{
	public DbSet<TransactionModel> TransactionModels { get; set; } = null!;
}