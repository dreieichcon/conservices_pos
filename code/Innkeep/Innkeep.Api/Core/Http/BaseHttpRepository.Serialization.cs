using System.Text.Json;
using Innkeep.Api.Models.Core;
using Serilog;

namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
	protected T? DeserializeOrNull<T>(ApiResponse result) where T : class
	{
		if (!result.IsSuccess) return null;

		var deserialized = Deserialize<T>(result.Content);

		return deserialized;
	}
	
	protected T? Deserialize<T>(string? content) where T: class
	{
		try
		{
			return string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<T>(content, GetOptions());
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Error while deserializing");
			return null;
		}
	}

	protected string Serialize<T>(T item)
	{
		return JsonSerializer.Serialize<T>(item, GetOptions());
	}

	protected abstract JsonSerializerOptions GetOptions();
}