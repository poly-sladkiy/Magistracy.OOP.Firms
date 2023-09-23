﻿using System.Runtime.CompilerServices;

namespace Firms.Models;

public class FirmFactory
{
	private FirmFactory() { }
	private static FirmFactory? _factory = null;

	public static FirmFactory? Factory
	{
		get
		{
			_factory ??= new FirmFactory();
			return _factory;
		}
	}
}