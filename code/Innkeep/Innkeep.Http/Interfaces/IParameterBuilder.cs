namespace Lite.Http.Interfaces;

public interface IParameterBuilder<TPb>
	where TPb : class, IParameterBuilder<TPb>
{
	public Dictionary<string, string> Values { get; set; }

	public IUrlBuilder<TPb> UrlBuilder { get; set; }

	public IUrlBuilder<TPb> Build();
}