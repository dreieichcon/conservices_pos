using Flurl.Http;
using Lite.Http.Interfaces;

namespace Lite.Http.Repository;

/// <summary>
///     Core Http Repository class.
/// </summary>
public abstract partial class AbstractHttpRepository<TPb>
	where TPb : class, IParameterBuilder<TPb>
{
	/// <summary>
	///     Default request timeout in milliseconds.
	/// </summary>
	protected virtual int Timeout => 5000;

	/// <summary>
	///     Setup method used to prepare the Flurl Client
	/// </summary>
	protected abstract void SetupClient();

	/// <summary>
	///     Creates the base request.
	/// </summary>
	/// <param name="urlBuilder">Fully formed UrlBuilder</param>
	/// <returns></returns>
	protected virtual IFlurlRequest CreateRequest(IUrlBuilder<TPb> urlBuilder)
	{
		var request = new FlurlRequest(urlBuilder.BaseUrl).AppendPathSegments(urlBuilder.PathSegments);

		foreach (var parameterPair in urlBuilder.Parameters.Values) request.AppendPathSegments(parameterPair);

		return request;
	}
}