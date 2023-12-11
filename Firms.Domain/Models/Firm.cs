using System.Diagnostics;

namespace Firms.Domain.Models;

public class Firm
{
	private Firm()
	{ }

	public Firm(string name, string shortName, string country, string region, string town, string street, string postIndex, string email, string web,
				Dictionary<string, string>? fields = null
	)
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
		UserFields = fields ?? new Dictionary<string, string>();
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

	public bool ExistContact(Contact contact)
		=> _subFirms.Exists(sb => sb.ExistContact(contact));

	/// <summary>
	/// в главную передать
	/// </summary>
	/// <param name="contact"></param>
	public void AddContact(Contact contact)
	{
		var mainSubFirm = _subFirms.SingleOrDefault(x => x.SubFirmType.IsMain);
		mainSubFirm?.AddContact(contact);
	}

	public void AddContactToSubFirm(Contact contact, SubFirmType subFirmType, bool checkOtherTypes = false)
	{
		var subFirm = _subFirms.FirstOrDefault(x => x.SubFirmType == subFirmType);

		if (subFirm is not null)
		{
			subFirm.AddContact(contact);
			return;
		}

		if (_subFirms.Count == 1 && checkOtherTypes)
			this.AddContact(contact);
	}

	#region sub firms fields

	private List<SubFirm> _subFirms = new List<SubFirm>();//Подразделения фирмы

	public List<SubFirm> SubFirms 
	{ 
		get => new (_subFirms); 
		private set => _subFirms = value.ToList(); 
	}

	public int SubFirmsCount => _subFirms.Count;

	public SubFirm GetMain() => _subFirms.First(x => x.SubFirmType.IsMain);

	public void AddSubFirm(SubFirm subFirm)
		=> _subFirms.Add(subFirm);

	#endregion sub firms fields

	#region user fields

	private Dictionary<string, string> UserFields = new();//Пользовательские поля

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