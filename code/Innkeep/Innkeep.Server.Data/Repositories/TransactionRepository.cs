using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
}