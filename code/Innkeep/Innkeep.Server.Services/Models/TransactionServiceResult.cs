using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Models.Transaction;

namespace Innkeep.Server.Services.Models;

public class TransactionServiceResult
{
	public PretixTransaction PretixTransaction { get; init; } = null!;
	
	public PretixOrderResponse OrderResponse { get; init; } = null!;
	
	public TseResult TseResult { get; init; } = null!;
	
	public Guid Guid { get; init; }
	
	public string RegisterId { get; init; } = null!;

	public string? OrganizerInfo { get; init; }

	public string EventName { get; init; } = null!;
}