using System.Text.Json;
using Firm.Helper.Extensions;
using Firms.Domain.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace UnitTest.ModelTests;

[TestClass]
public class FirmFactoryTest
{
	private string[] Towns = new[] { "Нижний Новгород", "Владимир", "Бор", "Кстово" };

	private static SubFirmType SupplyDepartment = new(isMain: false, name: "Отдел снабжения");

	private SubFirmTypeCollection SubFirmTypes = new(new List<SubFirmType>()
	{
		new(isMain: false, name: "Отдел маркетинга"),
		SupplyDepartment,
		new(isMain: false, name: "Отдел качества"),
		new(isMain: false, name: "Отдел администрации"),
		new(isMain: false, name: "Отдел кадров"),
		new(isMain: false, name: "Отдел охраны"),
		new(isMain: false, name: "Отдел разработки"),
	});

	private readonly List<ContactType> ContactTypes = new()
	{
		new("Email", "электронная почта"),
		new("Letter", "Почта"),
		new("Call", "Звонок"),
		//new("Call", "Звонок"),
	};

	private readonly List<Contact> Contacts = new List<Contact>();

	public void GenerateRandomsFirmsViaFirmFactory(int count = 10, bool addSubfirms = false)
	{
		var rnd = new Random();
		
		for (int i = 0; i < count; i++)
		{
			var firm = FirmFactory.Factory.Create(
				$"Name_{i}",
				$"ShortName_{i}",
				$"Country_{i}",
				$"Region_{i}",
				Towns[rnd.Next(0, Towns.Length)],
				$"Street_{i}",
				$"postIndex_{i}",
				$"email_{i}@email.com",
				$"www.Web_{i}.com");

			AddSubfirms(firm);
		}
	}

	private void AddSubfirms(Firms.Domain.Models.Firm firm)
	{
		var rnd = new Random();
		var countOfSubfirmTypes = rnd.Next(SubFirmTypes.Count);
		var shuffledSubFirmTypes = SubFirmTypes.Shuffle().ToList();

		for (int i = 0; i < countOfSubfirmTypes; i++)
		{
			firm.AddSubFirm(
				new SubFirm(
					$"{firm.Name}_subfirm_{i}", 
					$"short_boss_{i}", 
					$"big_boss_{i}", 
					$"phone_{i}", 
					$"{firm.Name}_i@mail.ru", 
					shuffledSubFirmTypes[i]
				)
			);
		}
	}

	[TestMethod]
	public void TestCreateFirmFromFabric()
	{
		var userFields = new Dictionary<string, string>()
		{
			{"key_1", "value_1"},
			{"key_2", "value_2"},
			{"key_3", "value_3"},
			{"key_4", "value_4"},
			{"key_5", "value_5"},
		};

		var localFirm = new Firms.Domain.Models.Firm("Name", "ShortName", "Country", "Region", "Town", "Street", "postIndex", "Email",
			"Web", userFields);

		var fabricFirm = FirmFactory
			.Factory.Create(
				localFirm.Name,
				localFirm.ShortName,
				localFirm.Country,
				localFirm.Region,
				localFirm.Town,
				localFirm.Street,
				localFirm.PostIndex,
				localFirm.Email,
				localFirm.Web,
				localFirm.UserFields);

		Assert.IsNotNull(fabricFirm);
		Assert.IsTrue(FirmFactory.Firms.Count > 0);
		Assert.AreEqual(localFirm.Name, fabricFirm.Name);
		Assert.AreEqual(localFirm.ShortName, fabricFirm.ShortName);
		Assert.AreEqual(localFirm.Country, fabricFirm.Country);
		Assert.AreEqual(localFirm.Region, fabricFirm.Region);
		Assert.AreEqual(localFirm.Town, fabricFirm.Town);
		Assert.AreEqual(localFirm.Street, fabricFirm.Street);
		Assert.AreEqual(localFirm.PostIndex, fabricFirm.PostIndex);
		Assert.AreEqual(localFirm.Email, fabricFirm.Email);
		Assert.AreEqual(localFirm.Web, fabricFirm.Web);
		Assert.AreEqual(localFirm.UserFields, fabricFirm.UserFields);
	}

	[TestMethod]
	public void ValidateFilterByTown()
	{
		GenerateRandomsFirmsViaFirmFactory(50);

		foreach (var town in Towns)
		{
			var firms = FirmFactory.Firms.Where(x => x.Town == town).ToList();

			Assert.IsNotNull(firms);
			Assert.IsTrue(firms.All(x => x.Town == town));
		}
	}

	/// <summary>
	/// Проверка добавленного контакта по значению и по ссылке
	/// </summary>
	[TestMethod]
	public void ValidateAddContactToFirmAndContactsReferencesNotEqual()
	{
		var contact = new Contact("description", "data info", new("Коммерческое предложение", "письмо"));

		GenerateRandomsFirmsViaFirmFactory(1);
		var firm = FirmFactory.Firms.First();
		firm.AddContact(contact);

		// получаем контакт фирмы
		var firmContact = firm.SubFirms.First(x => x.ExistContact(contact)).Contacts!.First();

		Assert.IsNotNull(firmContact);
		Assert.IsTrue(contact == firmContact);
		Assert.IsFalse(ReferenceEquals(contact, firmContact));
	}

	[TestMethod]
	public void ValidateAddingContactToMultipleFirms()
	{
		var rnd = new Random();
		var countOfFirmsForContact = 10;
		var firmsForContact = new List<Firms.Domain.Models.Firm>();
		var allFirms = new List<Firms.Domain.Models.Firm>();

		var contact = new Contact("description", "data info", new("Письмо", "письмо"));

		GenerateRandomsFirmsViaFirmFactory(50);
		allFirms = FirmFactory.Firms;

		while (countOfFirmsForContact-- >= 0)
		{
			var randFirm = allFirms[rnd.Next(allFirms.Count)];
			firmsForContact.Add(randFirm);
			allFirms.Remove(randFirm);
		}

		foreach (var firm in firmsForContact)
		{
			firm.AddContact(contact);
		}

		foreach (var firm in firmsForContact)
		{
			Assert.IsTrue(firm.ExistContact(contact));
		}

		foreach (var firm in allFirms)
		{
			Assert.IsFalse(firm.ExistContact(contact));
		}
	}

	[TestMethod]
	public void ValidateRandomAddingContactToSpecialSubFirm()
	{
		var contact = new Contact("description", "data info", new("Коммерческое предложение", "письмо"));

		GenerateRandomsFirmsViaFirmFactory(50);

		FirmFactory.Firms.ForEach(f =>
		{
			f.AddContactToSubFirm(contact, SupplyDepartment);

			if (f.SubFirms.Any(sf => sf.SubFirmType == SupplyDepartment))
				Assert.IsTrue(f.ExistContact(contact));
		});

		Console.WriteLine(JsonSerializer.Serialize(FirmFactory.Firms));
	}

	[TestMethod]
	public void ValidateRandomAddingContactToSpecialSubFirmWithAddingParametr()
	{
		var contact = new Contact("description", "data info", new("Коммерческое предложение", "письмо"));

		GenerateRandomsFirmsViaFirmFactory(50);
		FirmFactory.Firms.ForEach(f => f.AddContactToSubFirm(contact, SupplyDepartment, true));

		var firmsWithoutSupplyDepartmentAndWithOneSubfirm = 
			FirmFactory.Firms.Where(firm => firm.SubFirms.Any(sf => sf.IsYourType(SupplyDepartment) == false) && firm.SubFirms.Count == 0).ToList();

		firmsWithoutSupplyDepartmentAndWithOneSubfirm
			.ForEach(f => Assert.IsTrue(f.ExistContact(contact)));

		Console.WriteLine(JsonSerializer.Serialize(FirmFactory.Firms));
	}
}
