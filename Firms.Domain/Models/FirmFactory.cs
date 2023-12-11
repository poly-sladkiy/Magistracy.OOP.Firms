namespace Firms.Domain.Models;

public class FirmFactory
{
	private FirmFactory() { }
	private static FirmFactory? _factory = null;
	
	private static List<Firm> _firms = new();

	public static List<Firm> Firms => new(_firms);

	private static SubFirmType MainSubFirmType { get; } = new(true, "Основной офис");

	public static readonly string UserField1 = "field_1";
	public static readonly string UserField2 = "field_2";
	public static readonly string UserField3 = "field_3";
	public static readonly string UserField4 = "field_4";
	public static readonly string UserField5 = "field_5";

	public static FirmFactory Factory
	{
		get
		{
			_factory ??= new FirmFactory();
			return _factory;
		}
	}

	public Firm Create(string name, string shortName, string country, string region, string town, string street,
		string postIndex, string email, string web,
		string? field1 = null, string? field2 = null, string? field3 = null, string? field4 = null, string? field5 = null)
	{
		var userFields = new Dictionary<string, string>
		{
			{UserField1, field1 ?? ""},
			{UserField2, field2 ?? ""},
			{UserField3, field3 ?? ""},
			{UserField4, field4 ?? ""},
			{UserField5, field5 ?? ""},
		};

		var firm = new Firm(name, shortName, country, region, town, street,
			postIndex, email, web, userFields);

		firm.AddSubFirm(new SubFirm(
			$"Main_subfirm_{firm.Name}", 
			$"Boss name of {firm.Name}", 
			"Off name of boss", 
			"Phone", 
			"email", 
			MainSubFirmType)
		);

		_firms.Add(firm);

		return firm;
	}
}