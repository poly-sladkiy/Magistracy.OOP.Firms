using System.Runtime.CompilerServices;

namespace Firms.Models;

public class FirmFactory
{
	private FirmFactory() { }
	private FirmFactory? _factory = null;

	public FirmFactory? Factory
	{
		get
		{
			this._factory ??= new FirmFactory();
			return this._factory;
		}
	}
}