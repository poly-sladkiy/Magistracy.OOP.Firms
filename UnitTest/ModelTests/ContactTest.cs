using Firms.Domain.Models;

namespace UnitTest.ModelTests;

[TestClass]
public class ContactTest
{
	[TestMethod]
	public void ContactDateEndMustBeSetOnlyOneTime()
	{
		var contact = new Contact(description:"desc", dataInfo:"data info", contactType: new ContactType("name", note:"note"));

		Assert.IsNotNull(contact);

		var dateEnd = DateTime.Now.AddYears(2);
		contact.EndDt = dateEnd;
		Assert.AreEqual(dateEnd, contact.EndDt);

		var newDateEnd = DateTime.Now.AddYears(3);
		contact.EndDt = newDateEnd;
		Assert.AreNotEqual(contact.EndDt, newDateEnd);
	}
}