using Firms.Domain.Models;

namespace UnitTest.CollectionTests;

[TestClass]
public class SubFirmTypeCollectionTest
{
	[TestMethod]
	public void TestForEach()
	{
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

		Assert.IsTrue(true);
	}
}