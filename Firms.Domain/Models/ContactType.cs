namespace Firms.Domain.Models;

public class ContactType
{
	private ContactType() { }

	public string Name { get; private set; } = null!;
	public string Note { get; private set; } = null!;
}