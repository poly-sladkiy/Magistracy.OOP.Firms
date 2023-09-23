namespace Firms.Domain.Models;

/// <summary>
/// This class for history of contacts 
/// </summary>
public class Contact
{
	private Contact() { }

	public Contact(string description, string dataInfo, ContactType contactType, DateTime? beginDate = null, DateTime? endDate = null)
	{
		Description = description;
		DataInfo = dataInfo;
		ContactType = contactType;
		BeginDt = beginDate ?? default;
		EndDt = endDate;
	}

	public DateTime BeginDt { get; private set; } = DateTime.Now;//Дата начала контакта

	private DateTime? _endDt = null;

	public DateTime? EndDt
	{
		get => _endDt;
		set => _endDt ??= value;
	} //Дата завершения контакта

	public string Description { get; private set; } = null!;//Описание контакта для себя
	public string DataInfo { get; private set; } = null!;//Формулировка контакта для клиента
	public ContactType ContactType { get; private set; } //Вид контакта
	public Contact Clone() => new(Description, DataInfo, ContactType, _endDt);

	public static bool operator ==(Contact left, Contact right)
	{
		return
			( left.BeginDt, left.EndDt, left.Description, left.ContactType.Name, left.ContactType.Note, left.DataInfo )
			== ( right.BeginDt, right.EndDt, right.Description, right.ContactType.Name, right.ContactType.Note, right.DataInfo );
	}

	public static bool operator !=(Contact left, Contact right)
	{
		return !(left == right);
	}
}