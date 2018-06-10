﻿namespace FluentContracts
{
	internal static class Validator
	{
		public static void CheckOutOfRange(bool condition, string argumentName, string message = null)
		{
			if (condition) return;

			ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
		}

		public static void CheckSpecificValue(bool condition, string argumentName, string message = null)
		{
			if (condition) return;

			ThrowHelper.ThrowInvalidArgumentValueException(argumentName, message);
		}
	}
}