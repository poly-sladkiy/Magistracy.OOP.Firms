using System.Collections;

namespace Firms.Domain.Models;

public class SubFirmTypeCollection : IEnumerable<SubFirmType>
{
	private readonly List<SubFirmType> _lst = new();

	public SubFirmTypeCollection(List<SubFirmType> value)
	{
		_lst = value;
	}

	public IEnumerator<SubFirmType> GetEnumerator()
		=> _lst.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}