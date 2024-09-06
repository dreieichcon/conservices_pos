using System.Text.Json;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;
using Serilog;

namespace Innkeep.Http.Repository;

public abstract partial class BaseHttpRepository
{
	/// <summary>
	///     Will try to deserialize a result into a <see cref="IHttpResponse" />
	/// </summary>
	/// <param name="result">ApiResult to deserialize.</param>
	/// <param name="forceDeserializeError">Will force deserialization, even the api result was unsuccessful.</param>
	/// <typeparam name="T">Type to deserialize into.</typeparam>
	/// <returns>A <see cref="IHttpResponse{T}" />.</returns>
	protected IHttpResponse<T> DeserializeOrNull<T>(ApiResponse result, bool forceDeserializeError = false)
		where T : class
	{
		if (!result.IsSuccess && !forceDeserializeError)
			return HttpResponse<T>.Error(result);

		var deserialized = Deserialize<T>(result);

		return deserialized;
	}

	/// <summary>
	///     Internal deserialization method, which will try to deserialize into the requested type,
	///     or return an exception result.
	/// </summary>
	/// <param name="result">ApiResult to deserialize</param>
	/// <typeparam name="T">Type to deserialize into.</typeparam>
	/// <returns>A <see cref="IHttpResponse{T}" />.</returns>
	protected IHttpResponse<T> Deserialize<T>(ApiResponse result) where T : class
	{
		try
		{
			var deserialized = string.IsNullOrEmpty(result.Content)
				? null
				: JsonSerializer.Deserialize<T>(result.Content, GetOptions());

			return HttpResponse<T>.Ok(result, deserialized);
		}
		catch (Exception exception)
		{
			Log.Error(exception, "Error while deserializing into {Type}", typeof(T));

			return HttpResponse<T>.Exception(exception);
		}
	}

	/// <summary>
	///     Serialize an item into a json string for a request with options provided by <see cref="GetOptions" />.
	/// </summary>
	/// <param name="item">Item to serialize.</param>
	/// <typeparam name="T">Item Type.</typeparam>
	/// <returns>A json string representing the item.</returns>
	protected string Serialize<T>(T item) => JsonSerializer.Serialize(item, GetOptions());

	/// <summary>
	///     Retrieves the <see cref="JsonSerializerOptions" /> for this specific repository.
	/// </summary>
	/// <returns><see cref="JsonSerializerOptions" /> for the current repository.</returns>
	protected abstract JsonSerializerOptions GetOptions();
}