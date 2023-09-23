namespace Firms.Domain.Models;

public class ContactType
{
	private ContactType() { }

	public ContactType(string name, string note)
	{
		Name = name;
		Note = note;
	}

	public string Name { get; private set; } = null!;
	public string Note { get; private set; } = null!;
}