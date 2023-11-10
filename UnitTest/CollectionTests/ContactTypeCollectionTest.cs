using Firms.Domain.Models;

namespace UnitTest.CollectionTests;

[TestClass]
public class ContactTypeCollectionTest
{
	[TestMethod]
	public void TestForEach()
	{
		#region arrange

		var collection = new ContactTypeCollection(new()
		{
			new("Email", "электронная почта"),
			new("Letter", "Почта"),
			new("Call", "Звонок"),
			//new("Call", "Звонок"),
		});

		foreach (var item in collection)
		{
			Console.WriteLine($"{item.Name}: {item.Name}");
		}

		#endregion arrange

		#region assert

		Assert.IsTrue(true);

		#endregion assert
	}

	[TestMethod]
	public void ValidateCount()
	{
		#region arrange

		var mainCollection = new List<ContactType>()
		{
			new("Email", "электронная почта"),
			new("Letter", "Почта"),
			new("Call", "Звонок"),
			//new("Call", "Звонок"),
		};

		var collection = new ContactTypeCollection(mainCollection);

		var localCollectionCount = mainCollection.Count;
		var collectionCount = collection.Count;

		#endregion arrange

		#region assert

		Assert.AreEqual(localCollectionCount, collectionCount);

		#endregion assert
	}
}