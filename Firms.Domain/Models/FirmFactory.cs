namespace Firms.Domain.Models;

public class FirmFactory
{
	private FirmFactory() { }
	private static FirmFactory? _factory = null;
	public static List<Firm> Firms { get; private set; } = new();

	public static FirmFactory Factory
	{
		get
		{
			_factory ??= new FirmFactory();
			return _factory;
		}
	}

	public Firm Create(string name, string shortName, string country, string region, string town, string street,
		string postIndex, string email, string web)
	{
		var firm = new Firm(name, shortName, country, region, town, street,
			postIndex, email, web);
		Firms.Add(firm);
		return firm;
	}
}