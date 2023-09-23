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

	[TestMethod]
	public void ValidateEquals()
	{
		var dateStart = DateTime.Now;

		var contactFirst = new Contact(description: "desc", dataInfo: "data info", contactType: new ContactType("name", note: "note"), beginDate: dateStart);
		var contactSecond = new Contact(description: "desc", dataInfo: "data info", contactType: new ContactType("name", note: "note"), beginDate: dateStart);

		Assert.IsTrue(contactFirst == contactSecond);
		Assert.IsFalse(contactFirst != contactSecond);

		var contactThird = new Contact(description: "desc 1", dataInfo: "data info 1", contactType: new ContactType("name 1", note: "note 1"), endDate: DateTime.Now.AddYears(10));

		Assert.IsFalse(contactThird == contactFirst);
		Assert.IsTrue(contactThird != contactSecond);
	}
}