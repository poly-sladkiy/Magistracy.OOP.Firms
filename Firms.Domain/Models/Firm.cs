namespace Firms.Domain.Models;

public class Firm
{
	private Firm()
	{ }

	public Firm(string name, string shortName, string country, string region, string town, string street, string postIndex, string email, string web)
	{
		Name = name;
		ShortName = shortName;
		Country = country;
		Region = region;
		Town = town;
		Street = street;
		PostIndex = postIndex;
		Email = email;
		Web = web;
	}

	public string Name { get; private set; } = null!; //Полное наименование фирмы
	public string ShortName { get; private set; } = null!; //Краткое наименование фирмы
	public string Country { get; private set; } = null!;//Страна
	public string Region { get; private set; } = null!;//Регион (область)
	public string Town { get; private set; } = null!;//Город
	public string Street { get; private set; } = null!;//Улица
	public string PostIndex { get; private set; } = null!;//Почтовый индекс
	public DateTime DateIn { get; private set; } = DateTime.Now;//Дата ввода фирмы (начало взаимоотношений)
	public string Email { get; private set; } = null!;//Почтовый адрес фирмы
	public string Web { get; private set; } = null!;//URL-адрес сайта

	public bool ExistContact(ContactType type)
		=> SubFirms.Exists(_ => _.Contacts.Exists(x => x.ContactType.Name == type.Name));

	/// <summary>
	/// в главную передать
	/// </summary>
	/// <param name="contact"></param>
	public void AddContact(Contact contact)
	{
		var mainSubFirm = SubFirms.SingleOrDefault(x => x.SubFirmType.IsMain);
		mainSubFirm?.AddContact(contact);
	}

	public void AddContactToSubFirm(Contact contact, SubFirmType subFirmType)
	{
		var subFirm = SubFirms.FirstOrDefault(x => x.SubFirmType == subFirmType);
		subFirm?.AddContact(contact);
	}

	#region sub firms fields

	public List<SubFirm> SubFirms { get; private set; } = new();//Подразделения фирмы

	public int SubFirmsCount => SubFirms.Count;

	public SubFirm GetMain() => SubFirms.First(x => x.SubFirmType.IsMain);

	public void AddSubFirm(SubFirm subFirm)
		=> SubFirms.Add(subFirm);

	#endregion sub firms fields

	#region user fields

	public Dictionary<string, string> UserFields { get; private set; } = new();//Пользовательские поля

	public void SetField(string name, string value)
		=> UserFields[name] = value;

	public void RenameField(string oldName, string newName)
	{
		var data = UserFields[oldName];
		UserFields.Remove(oldName);
		UserFields.Add(newName, data);
	}

	public string GetField(string name)
		=> UserFields[name];

	#endregion user fields
}