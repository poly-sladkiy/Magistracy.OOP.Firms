namespace Firms.Domain.Models;

public class SubFirm
{
	private SubFirm() { }

	public SubFirm(string name, string bossName, string ofcBossName, string tel, string email, SubFirmType subFirmType)
	{
		Name = name;
		BossName = bossName;
		OfficialBossName = ofcBossName;
		Phone = tel;
		Email = email;
		SubFirmType = subFirmType;
	}

	public string Name { get; private set; } = null!;//Наименование подразделения
	public string BossName { get; private set; } = null!;//Имя руководителя подразделения
	public string OfficialBossName { get; private set; } = null!;//Официальное обращение к руководителю
	public string Phone { get; private set; } = null!;//номер телефона подразделения
	public string Email { get; private set; } = null!;//Почтовый адрес подразделения
	public SubFirmType SubFirmType { get; private set; } = null!;//Тип подразделения
	public List<Contact> Contacts { get; private set; } = new(); //Контакты подразделения
}