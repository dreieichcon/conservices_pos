using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Fiskaly;
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
		var uri = FiskalyUrlBuilder
				.Endpoints.SpecificTss(tssId)
				.SpecificTransaction(transactionId)
				.Parameters.TransactionRevision(1)
				.Build();

		var payload = new FiskalyTransactionStartRequest
		{
			State = TransactionState.Active,
			ClientId = clientId,
		};

		return await Put<FiskalyTransactionStartRequest, FiskalyTransaction>(uri, payload);
	}

	public async Task<IHttpResponse<FiskalyTransaction>> UpdateTransaction(
		FiskalyTransactionUpdateRequest updateRequest
	)
	{
		var endpoint = FiskalyUrlBuilder
						.Endpoints.SpecificTss(updateRequest.TssId)
						.SpecificTransaction(updateRequest.TransactionId)
						.Parameters.TransactionRevision(updateRequest.TransactionRevision)
						.Build();

		return await Put<FiskalyTransactionUpdateRequest, FiskalyTransaction>(endpoint, updateRequest);
	}
}