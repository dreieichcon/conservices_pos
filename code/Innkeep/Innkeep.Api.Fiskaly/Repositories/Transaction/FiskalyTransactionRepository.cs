using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Enum.Fiskaly.Transaction;
using Innkeep.Api.Fiskaly.Interfaces.Transaction;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Fiskaly.Request.Transaction;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Fiskaly.Repositories.Transaction;

public class FiskalyTransactionRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractFiskalyRepository(authenticationService), IFiskalyTransactionRepository
{
	public async Task<IHttpResponse<FiskalyTransaction>> StartTransaction(
		string tssId,
		string transactionId,
		string clientId
	)
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

		return DeserializeResult<FiskalyTransaction>(result);
	}

	public async Task<IHttpResponse<FiskalyTransaction>> UpdateTransaction(
		FiskalyTransactionUpdateRequest updateRequest
	)
	{
		var endpoint = new FiskalyEndpointBuilder()
						.WithSpecificTss(updateRequest.TssId)
						.WithSpecificTransaction(updateRequest.TransactionId)
						.WithTransactionRevision(updateRequest.TransactionRevision)
						.Build();

		var result = await Put(endpoint, Serialize(updateRequest));

		return DeserializeResult<FiskalyTransaction>(result);
	}
}