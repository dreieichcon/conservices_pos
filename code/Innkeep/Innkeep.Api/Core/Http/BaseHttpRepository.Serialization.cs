using System.Text.Json;
using Serilog;

namespace Innkeep.Api.Core.Http;

public abstract partial class BaseHttpRepository
{
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

	protected abstract JsonSerializerOptions GetOptions();
}