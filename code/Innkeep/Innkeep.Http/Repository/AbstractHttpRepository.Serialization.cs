using System.Text.Json;
using Flurl.Http;
using Lite.Http.Interfaces;
using Lite.Http.Response;
using Serilog;

namespace Lite.Http.Repository;

public abstract partial class AbstractHttpRepository<TPb>
{
	/// <summary>
	///     Will force deserialization, even if there was an error.
	/// </summary>
	protected abstract bool DeserializeIfError { get; }

	/// <summary>
	///     Retrieves the <see cref="JsonSerializerOptions" /> for this specific repository.
	/// </summary>
	/// <returns><see cref="JsonSerializerOptions" /> for the current repository.</returns>
	protected abstract JsonSerializerOptions GetOptions();

	/// <summary>
	///     Will try to deserialize a result into a <see cref="IHttpResponse" />
	/// </summary>
	/// <param name="result">ApiResult to deserialize.</param>
	/// <typeparam name="T">Type to deserialize into.</typeparam>
	/// <returns>A <see cref="IHttpResponse{T}" />.</returns>
	protected async Task<IHttpResponse<T>> DeserializeResult<T>(IFlurlResponse result, T? defaultValue = default)
	{
		if (result.StatusCode > 199 && !DeserializeIfError)
			return await HttpResponse<T>.Error(result, defaultValue);

		return await Deserialize(result, defaultValue);
	}

	/// <summary>
	///     Internal deserialization method, which will try to deserialize into the requested type,
	///     or return an exception result.
	/// </summary>
	/// <param name="result">ApiResult to deserialize</param>
	/// <param name="defaultValue"></param>
	/// <typeparam name="T">Type to deserialize into.</typeparam>
	/// <returns>A <see cref="IHttpResponse{T}" />.</returns>
	protected virtual async Task<HttpResponse<T>> Deserialize<T>(IFlurlResponse result, T? defaultValue = default)
	{
		var resultText = await result.GetStringAsync();

		try
		{
			var deserialized = string.IsNullOrEmpty(resultText)
				? defaultValue
				: JsonSerializer.Deserialize<T>(resultText, GetOptions());

			return await HttpResponse<T>.Ok(result, deserialized);
		}
		catch (Exception exception)
		{
			Log.Error(exception, "Error while deserializing into {Type}", typeof(T));

			return HttpResponse<T>.Exception(exception, defaultValue);
		}
	}

	/// <summary>
	///     Attaches form content to a GET request.
	/// </summary>
	/// <param name="request">The request object itself.</param>
	/// <param name="formContent">Form content to transmit.</param>
	/// <typeparam name="T">Form content type. Should be of type Dictionary<string, string></typeparam>
	private static void AttachGetFormContent<T>(IFlurlRequest request, T? formContent)
	{
		if (formContent is Dictionary<string, string> formContentDictionary)
			request.Content = new FormUrlEncodedContent(formContentDictionary);
	}
}