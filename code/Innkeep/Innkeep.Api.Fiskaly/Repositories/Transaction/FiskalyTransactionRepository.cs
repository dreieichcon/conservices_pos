using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Fiskaly.Interfaces.Transaction;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;

namespace Innkeep.Api.Fiskaly.Repositories.Transaction;

public class FiskalyTransactionRepository(IFiskalyAuthenticationService authenticationService)
	: Abstract(authenticationService), IFiskalyTransactionRepository
{
	public async Task<FiskalyTransaction?> StartTransaction(string tssId, string transactionId, string clientId)
	{
		var endpoint = new FiskalyEndpointBuilder()
						.WithSpecificTss(tssId)
						.WithSpecificTransaction(transactionId)
						.WithTransactionRevision(1)
						.Build();

		var payload = new FiskalyTransactionStartRequest
		{
			State = TransactionState.Active,
			ClientId = clientId,
		};

		var serialized = Serialize(payload);

		var result = await Put(endpoint, serialized);

		return DeserializeOrNull<FiskalyTransaction>(result);
	}

	public async Task<FiskalyTransaction?> UpdateTransaction(FiskalyTransactionUpdateRequest transactionUpdateRequest)
	{
		var endpoint = new FiskalyEndpointBuilder()
						.WithSpecificTss(transactionUpdateRequest.TssId)
						.WithSpecificTransaction(transactionUpdateRequest.TransactionId)
						.WithTransactionRevision(transactionUpdateRequest.TransactionRevision)
						.Build();

		var result = await Put(endpoint, Serialize(transactionUpdateRequest));

		return DeserializeOrNull<FiskalyTransaction>(result);
	}
}