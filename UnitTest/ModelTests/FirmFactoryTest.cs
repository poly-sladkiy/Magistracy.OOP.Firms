using System.Globalization;
using Firms.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace UnitTest.ModelTests;

[TestClass]
public class FirmFactoryTest
{
	private string[] Towns = new[] { "Нижний Новгород", "Владимир", "Бор", "Кстово" };

	private SubFirmTypeCollection SubFirmTypes = new(new List<SubFirmType>()
	{
		new(isMain: true, name: "Основной офис"),
		new(isMain: false, name: "Отдел маркетинга"),
		new(isMain: false, name: "Отдел снабжения"),
		new(isMain: false, name: "Отдел качества"),
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
		
		var countSimpleFirms = rnd.Next(count / 2);
		var countBigFirms = count - countSimpleFirms;

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

			firm.AddSubFirm(new SubFirm($"Subfirm_{i}", "Name", "Off name", "Phone", "email", new(true, "Основной офис")));
		}
	}

	//private void AddSubfirms(Firm firm, bool isSimple = true)
	//{
	//	var rnd = new Random();

	//	if (isSimple) // todo: add sunfirm type
	//		firm.AddSubFirm(new SubFirm($"Name_{firm.Name}", $"boss {firm.Name}", $"Big boss {firm.Name}", $"{rnd.Next(100_000, 999_999)}", $"sub_firm_{firm.Email}", SubFirmTypes.GetEnumerator().));
			
	//	// todo: rnd select count of subfirms [2, SubFirmTypes.Length] - maybe replace to List
	//}

	[TestMethod]
	public void TestCreateFirmFromFabric()
	{
		var localFirm = new Firm("Name", "ShortName", "Country", "Region", "Town", "Street", "postIndex", "Email",
			"Web");

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
				localFirm.Web);

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

	[TestMethod]
	public void ValidateAddingContact()
	{
		GenerateRandomsFirmsViaFirmFactory(3);

		foreach (var town in FirmFactory.Firms)
		{
			town.AddContact(new Contact("desc", "data info", new ContactType("Письмо", "письмо"), DateTime.Now));
		}
	}
}
