using System;
using System.Text;
using NUnit.Framework;

namespace Faithlife.Utility.Tests
{
	[TestFixture]
	public class StringBuilderUtilityTests
	{
		[TestCase("False, True", "{0}, {1}", new object[] { false, true })]
		[TestCase("0, 255", "{0}, {1}", new object[] { 0, 255 })]
		[TestCase("-32768, 32767", "{0}, {1}", new object[] { Int16.MinValue, Int16.MaxValue })]
		public void AppendFormatInvariant(string strExpected, string strFormat, object[] values)
		{
			Assert.AreEqual(strExpected, new StringBuilder().AppendFormatInvariant(strFormat, values).ToString());
		}

		[TestCase(false, "False")]
		[TestCase(true, "True")]
		public void AppendInvariantBoolean(bool value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Byte.MinValue, "0")]
		[TestCase(Byte.MaxValue, "255")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantByte(byte value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Int16.MinValue, "-32768")]
		[TestCase(Int16.MaxValue, "32767")]
		[TestCase(-1, "-1")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantInt16(short value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Int32.MinValue, "-2147483648")]
		[TestCase(Int32.MaxValue, "2147483647")]
		[TestCase(-1, "-1")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantInt32(int value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Int64.MinValue, "-9223372036854775808")]
		[TestCase(Int64.MaxValue, "9223372036854775807")]
		[TestCase(-1, "-1")]
		[TestCase(0L, "0")]
		[TestCase(1L, "1")]
		public void AppendInvariantInt64(long value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Single.Epsilon, "1.401298E-45")]
		[TestCase(1.5f, "1.5")]
		[TestCase(-1, "-1")]
		[TestCase(0f, "0")]
		[TestCase(1f, "1")]
		public void AppendInvariantSingle(float value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(Double.Epsilon, "4.94065645841247E-324")]
		[TestCase(1.5, "1.5")]
		[TestCase(-1, "-1")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantDouble(double value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(1.5, "1.5")]
		[TestCase(-1, "-1")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantDecimal(decimal value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[Test]
		public void AppendInvariantDecimalMinMax()
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, Decimal.MaxValue);
			Assert.AreEqual("79228162514264337593543950335", sb.ToString());

			sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, Decimal.MinValue);
			Assert.AreEqual("-79228162514264337593543950335", sb.ToString());
		}

		[TestCase(SByte.MinValue, "-128")]
		[TestCase(SByte.MaxValue, "127")]
		[TestCase(-1, "-1")]
		[TestCase(0, "0")]
		[TestCase(1, "1")]
		public void AppendInvariantSByte(sbyte value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(UInt16.MinValue, "0")]
		[TestCase(UInt16.MaxValue, "65535")]
		[TestCase((ushort) 0, "0")]
		[TestCase((ushort) 1, "1")]
		public void AppendInvariantUInt16(ushort value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(UInt32.MinValue, "0")]
		[TestCase(UInt32.MaxValue, "4294967295")]
		[TestCase(0u, "0")]
		[TestCase(1u, "1")]
		public void AppendInvariantUInt32(uint value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}

		[TestCase(UInt64.MinValue, "0")]
		[TestCase(UInt64.MaxValue, "18446744073709551615")]
		[TestCase(0ul, "0")]
		[TestCase(1ul, "1")]
		public void AppendInvariantUInt64(ulong value, string strExpected)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilderUtility.AppendInvariant(sb, value);
			Assert.AreEqual(strExpected, sb.ToString());
		}
	}
}
