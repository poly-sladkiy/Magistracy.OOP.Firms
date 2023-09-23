namespace Firms.Models;

public class SubFirmType
{
	private SubFirmType() { }

	public SubFirmType(bool isMain, string name)
	{
		IsMain = isMain;
		Name = name;
	}

	public bool IsMain { get; private set; }
	public string Name { get; private set; } = null!;
}