namespace Firms.Domain.Models;

public class Contact
{
	private Contact() { }

	public Contact(string description, string dataInfo, ContactType contactType, DateTime? endDt = null)
	{
		Description = description;
		DataInfo = dataInfo;
		ContactType = contactType;
		EndDt = endDt;
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
}