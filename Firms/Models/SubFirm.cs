namespace Firms.Models;

public class SubFirm
{
	private SubFirm() { }

	public SubFirm(string name, string bossName, string ofcBossName, string tel, string email)
	{
		Name = name;
		BossName = bossName;
		OfcBossName = ofcBossName;
		Tel = tel;
		Email = email;
	}

	public string Name { get; private set; } = null!;//Наименование подразделения
	public string BossName { get; private set; } = null!;//Имя руководителя подразделения
	public string OfcBossName { get; private set; } = null!;//Официальное обращение к руководителю
	public string Tel { get; private set; } = null!;//номер телефона подразделения
	public string Email { get; private set; } = null!;//Почтовый адрес подразделения
	public SubFirmType SubFirmType { get; private set; } = new();//Тип подразделения
	public List<Contact> Contacts { get; private set; } = new(); //Контакты подразделения
}