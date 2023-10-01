using Firms.Domain.Models;

namespace UnitTest.CollectionTests;

[TestClass]
public class SubFirmTypeCollectionTest
{
	[TestMethod]
	public void TestForEach()
	{
		#region arrange

		var collection = new SubFirmTypeCollection(new List<SubFirmType>
		{
			new(true, "name 1"),
			new(false, "name 2"),
			new(false, "name 3"),
			new(false, "name 4"),
		});

		foreach (var item in collection)
		{
			Console.WriteLine($"{item.Name}: {item.IsMain}");
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

		var mainCollection = new List<SubFirmType>
		{
			new(true, "name 1"),
			new(false, "name 2"),
			new(false, "name 3"),
			new(false, "name 4"),
		};

		var collection = new SubFirmTypeCollection(mainCollection);

		var localCollectionCount = mainCollection.Count;
		var collectionCount = collection.Count;

		#endregion arrange

		#region assert

		Assert.AreEqual(localCollectionCount, collectionCount);

		#endregion assert
	}
}