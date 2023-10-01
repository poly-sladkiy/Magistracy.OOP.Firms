using Firms.Domain.Models;

namespace UnitTest.ModelTests;

[TestClass]
public class FirmTest
{
	/// <summary>
	/// Validate add subfirm and add contact to main subfirm
	/// </summary>
	[TestMethod]
	public void ValidateAddContact()
	{
		#region arrange

		var firm = new Firm("name", "short name", "country", "region", "town", "street", "post index", "email", "web");
		var subFirm = new SubFirm("sub name", "boos name", "off boss name", "phone", "email",
			new SubFirmType(true, "sub firm type"));
		var contact = new Contact("description", "data info",
			new ContactType("name contact type", "note contact type"), DateTime.MinValue, DateTime.MaxValue);

		#endregion

		#region act add subfirm & contact

		firm.AddSubFirm(subFirm);
		firm.AddContact(contact);

		#endregion

		#region assert

		Assert.IsTrue(subFirm.Contacts.Count > 0);
		Assert.IsTrue(firm.ExistContact(contact));
		Assert.IsTrue(subFirm.ExistContact(contact));

		#endregion
	}

	#region user fields tests

	[TestMethod]
	public void ValidateSetAndGetUserFields()
	{
		var firm = new Firm("name", "short name", "country", "region", "town", "street", "post index", "email", "web");
		
		var fieldKey = "test key";
		var fieldValue = "test value";
		firm.SetField(fieldKey, fieldValue);

		var fieldValueFromAllFields = firm.UserFields.First(x => x.Key == fieldKey).Value;
		var firmValue = firm.GetField(fieldKey);

		Assert.AreEqual(fieldValue, firmValue);
		Assert.AreEqual(fieldValue, fieldValueFromAllFields);
	}

	[TestMethod]
	public void ValidateRenameField()
	{
		var firm = new Firm("name", "short name", "country", "region", "town", "street", "post index", "email", "web");

		var fieldKey = "test key";
		var fieldValue = "test value";
		firm.SetField(fieldKey, fieldValue);

		var newFieldKey = "new test key";
		firm.RenameField(fieldKey, newFieldKey);

		var firmValue = firm.GetField(newFieldKey);

		Assert.AreEqual(fieldValue, firmValue);
		Assert.IsFalse(firm.UserFields.Any(x => x.Key == fieldKey));
	}

	#endregion

}