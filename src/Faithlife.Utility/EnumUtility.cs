using System;
using System.Globalization;

namespace Faithlife.Utility
{
	/// <summary>
	///  Provides helper methods for working with enumerated values.
	/// </summary>
	public static class EnumUtility
	{
		/// <summary>
		/// Gets the values defined by the enumerated type.
		/// </summary>
		/// <typeparam name="T">The enumerated type.</typeparam>
		/// <returns>The values defined by the enumerated type, sorted by value.</returns>
		public static T[] GetValues<T>()
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return (T[]) Enum.GetValues(typeof(T));
		}

		/// <summary>
		/// Returns true if the specified enum value has the specified flag bit(s) set.
		/// </summary>
		/// <typeparam name="T">The enumerated type.</typeparam>
		/// <param name="value">The value of the enum variable.</param>
		/// <param name="flag">The flag(s) to test.</param>
		/// <returns>Returns <c>true</c> if all flag bits are set; otherwise, <c>false</c>.</returns>
		/// <remarks>In .NET 4, use <code>Enum.HasFlag</code>.</remarks>
		public static bool HasFlag<T>(T value, T flag)
			where T : struct
		{
			ulong integerValue = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			ulong integerFlag = Convert.ToUInt64(flag, CultureInfo.InvariantCulture);
			return (integerValue & integerFlag) == integerFlag;
		}

		/// <summary>
		/// Determines whether the specified value is defined.
		/// </summary>
		/// <typeparam name="T">The enumerated type.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is defined; otherwise, <c>false</c>.</returns>
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		/// <summary>
		/// Parses the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <returns>A strongly typed enumerated value.</returns>
		/// <remarks>This method matches case.</remarks>
		public static T Parse<T>(string value)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return Parse<T>(value, CaseSensitivity.MatchCase);
		}

		/// <summary>
		/// Parses the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <param name="caseSensitivity">The case sensitivity.</param>
		/// <returns>A strongly typed enumerated value.</returns>
		public static T Parse<T>(string value, CaseSensitivity caseSensitivity)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return (T) Enum.Parse(typeof(T), value, caseSensitivity == CaseSensitivity.IgnoreCase);
		}

		/// <summary>
		/// Attempts to parse the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <returns>A strongly typed enumerated value; or null if the string could not be successfully parsed.</returns>
		/// <remarks>This method matches case.</remarks>
		public static T? TryParse<T>(string value)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return TryParse<T>(value, CaseSensitivity.MatchCase);
		}

		/// <summary>
		/// Attempts to parse the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <param name="caseSensitivity">The case sensitivity.</param>
		/// <returns>A strongly typed enumerated value; null if the string could not be successfully parsed.</returns>
		public static T? TryParse<T>(string value, CaseSensitivity caseSensitivity)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			T result;
			return Enum.TryParse(value, caseSensitivity == CaseSensitivity.IgnoreCase, out result) ? result : default(T?);
		}

		/// <summary>
		/// Attempts to parse the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <param name="result">The resulting enumerated value.</param>
		/// <returns>True if the string was successfully parsed.</returns>
		/// <remarks>This method matches case.</remarks>
		public static bool TryParse<T>(string value, out T result)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return TryParse(value, CaseSensitivity.MatchCase, out result);
		}

		/// <summary>
		/// Attempts to parse the specified string.
		/// </summary>
		/// <typeparam name="T">The enumerated value type.</typeparam>
		/// <param name="value">The string.</param>
		/// <param name="result">The resulting enumerated value.</param>
		/// <param name="caseSensitivity">The case sensitivity.</param>
		/// <returns>True if the string was successfully parsed.</returns>
		/// <remarks>This method ignores case.</remarks>
		public static bool TryParse<T>(string value, CaseSensitivity caseSensitivity, out T result)
			where T : struct // error CS0702: Constraint cannot be special class 'System.Enum'
		{
			return Enum.TryParse(value, caseSensitivity == CaseSensitivity.IgnoreCase, out result);
		}
	}
}
