namespace Firms.Models;

public class Contact
{
	private Contact() { }

	public Contact(DateTime? endDt, string description, string dataInfo)
	{
		EndDt = endDt;
		Description = description;
		DataInfo = dataInfo;
	}

	public DateTime BeginDt = DateTime.Now;//Дата начала контакта
	public DateTime? EndDt { get; private set; } = null;//Дата завершения контакта
	public string Description { get; private set; } = null!;//Описание контакта для себя
	public string DataInfo { get; private set; } = null!;//Формулировка контакта для клиента
	//public ContType ContactType;//Вид контакта
}