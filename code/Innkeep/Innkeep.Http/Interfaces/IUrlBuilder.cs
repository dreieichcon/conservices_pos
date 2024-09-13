namespace Lite.Http.Interfaces;

/// <summary>
///     Interface for the base URl Builder.
/// </summary>
/// <typeparam name="TPb">Type of the ParameterBuilder</typeparam>
public interface IUrlBuilder<TPb>
	where TPb : class, IParameterBuilder<TPb>
{
	/// <summary>
	///     Reference to the parameter builder to allow for easy fluent linking.
	/// </summary>
	public TPb Parameters { get; init; }

	/// <summary>
	///     Base Url for this builder.
	/// </summary>
	public string BaseUrl { get; }

	/// <summary>
	///     List of path segments.
	/// </summary>
	public IList<string> PathSegments { get; set; }
}