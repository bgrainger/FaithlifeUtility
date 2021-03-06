using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Faithlife.Utility.Tests
{
	[TestFixture]
	public class ListUtilityTests
	{
		delegate int SearchFunction(IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex);

		[Test]
		public void AsReadOnly()
		{
			var list = ((IList<string>) new[] { "hi", "ho" }).AsReadOnly();
			CollectionAssert.AreEqual(new[] { "hi", "ho" }, list);
			Assert.Throws<NotSupportedException>(() => { ((IList<string>) list)[0] = "hum"; });
		}

		[TestCase(0)]
		[TestCase(1)]
		public void MutateUnderlyingList(int initialCount)
		{
			List<int> original = new List<int>();
			for (int i = 0; i < initialCount; i++)
				original.Add(i);

			ReadOnlyCollection<int> wrapper = ListUtility.AsReadOnly(original);
			CollectionAssert.AreEqual(original, wrapper);

			original.Add(original.Count);
			CollectionAssert.AreEqual(original, wrapper);
		}

		[Test]
		public void BinarySearchNullList()
		{
			int nIndex;
			Assert.Throws<ArgumentNullException>(() => ListUtility.BinarySearchForKey<SearchData, int>(null, 1, CompareItemToKey, out nIndex));
		}

		[Test]
		public void BinarySearchNullCompare()
		{
			int nIndex;
			List<SearchData> list = new List<SearchData>();
			Assert.Throws<ArgumentNullException>(() => ListUtility.BinarySearchForKey(list, 1, null, out nIndex));
		}

		[Test]
		public void LinearSearchBadArguments()
		{
			int nIndex;
			List<SearchData> list = new List<SearchData>();
			Assert.Throws<ArgumentNullException>(() => ListUtility.LinearSearchForKey<SearchData, int>(null, 1, CompareItemToKey, out nIndex));
			Assert.Throws<ArgumentNullException>(() => ListUtility.LinearSearchForKey(list, 1, null, out nIndex));
		}

		[Test]
		public void BinarySearchEmptyList()
		{
			// Can't use ListUtility.BinarySearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchEmptyList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.BinarySearchForKey(list, key, fnCompare, out nIndex));
		}

		[Test]
		public void LinearSearchEmptyList()
		{
			// Can't use ListUtility.LinearSearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchEmptyList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.LinearSearchForKey(list, key, fnCompare, out nIndex));
		}

		private static void DoSearchEmptyList(SearchFunction fnSearch)
		{
			int nIndex;
			List<SearchData> list = new List<SearchData>();
			int nCount = fnSearch(list, 10, CompareItemToKey, out nIndex);
			Assert.AreEqual(0, nCount);
			Assert.AreEqual(0, nIndex);
		}

		[TestCase(10, 1, 0)]
		[TestCase(13, 0, 1)]
		[TestCase(3, 0, 0)]
		public void BinarySearchOneItemList(int nKey, int nExpectedCount, int nExpectedIndex)
		{
			// Can't use ListUtility.BinarySearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchOneItemList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.BinarySearchForKey(list, key, fnCompare, out nIndex), nKey, nExpectedCount, nExpectedIndex);
		}

		[TestCase(10, 1, 0)]
		[TestCase(13, 0, 1)]
		[TestCase(3, 0, 0)]
		public void LinearSearchOneItemList(int nKey, int nExpectedCount, int nExpectedIndex)
		{
			// Can't use ListUtility.LinearSearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchOneItemList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.LinearSearchForKey(list, key, fnCompare, out nIndex), nKey, nExpectedCount, nExpectedIndex);
		}

		private static void DoSearchOneItemList(SearchFunction fnSearch, int nKey, int nExpectedCount, int nExpectedIndex)
		{
			int nIndex;
			List<SearchData> list = new List<SearchData>();
			list.Add(new SearchData(10, 2));
			int nCount = fnSearch(list, nKey, CompareItemToKey, out nIndex);
			Assert.AreEqual(nExpectedCount, nCount);
			Assert.AreEqual(nExpectedIndex, nIndex);
		}

		[Test]
		public void BinarySearchLargeListWithMultiple()
		{
			// Can't use ListUtility.BinarySearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchLargeListWithMultiple((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.BinarySearchForKey(list, key, fnCompare, out nIndex));
		}

		[Test]
		public void LinearSearchLargeListWithMultiple()
		{
			// Can't use ListUtility.LinearSearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchLargeListWithMultiple((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.LinearSearchForKey(list, key, fnCompare, out nIndex));
		}

		private static void DoSearchLargeListWithMultiple(SearchFunction fnSearch)
		{
			int nIndex;
			List<SearchData> list = new List<SearchData>();
			list.Add(new SearchData(3, 1));
			list.Add(new SearchData(6, 1));
			list.Add(new SearchData(10, 1));
			list.Add(new SearchData(10, 1));
			list.Add(new SearchData(10, 1));
			list.Add(new SearchData(10, 1));
			list.Add(new SearchData(14, 1));
			list.Add(new SearchData(17, 1));
			list.Add(new SearchData(21, 1));
			int nCount = fnSearch(list, 10, CompareItemToKey, out nIndex);
			Assert.AreEqual(4, nCount);
			Assert.AreEqual(2, nIndex);

			nCount = fnSearch(list, 7, CompareItemToKey, out nIndex);
			Assert.AreEqual(0, nCount);
			Assert.AreEqual(2, nIndex);

			nCount = fnSearch(list, 12, CompareItemToKey, out nIndex);
			Assert.AreEqual(0, nCount);
			Assert.AreEqual(6, nIndex);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		[TestCase(10)]
		[TestCase(100)]
		[TestCase(255)]
		[TestCase(256)]
		public void BinarySearchLargeList(int nItems)
		{
			// Can't use ListUtility.BinarySearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchLargeList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.BinarySearchForKey(list, key, fnCompare, out nIndex), nItems);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		[TestCase(10)]
		[TestCase(100)]
		[TestCase(255)]
		[TestCase(256)]
		public void LinearSearchLargeList(int nItems)
		{
			// Can't use ListUtility.LinearSearchKey as the first parameter because of Mono bug #523683. Must use lambda expression.
			DoSearchLargeList((IList<SearchData> list, int key, Func<SearchData, int, int> fnCompare, out int nIndex) => ListUtility.LinearSearchForKey(list, key, fnCompare, out nIndex), nItems);
		}

		private static void DoSearchLargeList(SearchFunction fnSearch, int nItems)
		{
			List<SearchData> list = new List<SearchData>();
			for (int nItem = 0; nItem < nItems; ++nItem)
			{
				list.Add(new SearchData(100 * nItem, 0));
			}

			for (int i = 0; i < list.Count; ++i)
			{
				int nIndex;
				int nKey = i * 100;
				int nCount = fnSearch(list, nKey, CompareItemToKey, out nIndex);
				Assert.AreEqual(1, nCount);
				Assert.AreEqual(i, nIndex);

				nCount = fnSearch(list, nKey - 20, CompareItemToKey, out nIndex);
				Assert.AreEqual(0, nCount);
				Assert.AreEqual(i, nIndex);

				nCount = fnSearch(list, nKey + 20, CompareItemToKey, out nIndex);
				Assert.AreEqual(0, nCount);
				Assert.AreEqual(i + 1, nIndex);
			}
		}

		[Test]
		public void ListCopyTo()
		{
			DoTestCopyTo(new List<int> { 1, 2, 3, 4 });
		}

		[Test]
		public void ArrayCopyTo()
		{
			DoTestCopyTo(new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void CollectionCopyTo()
		{
			DoTestCopyTo(new ReadOnlyCollection<int>(new[] { 1, 2, 3, 4 }));
		}

		[Test]
		public void ListCopyToBadArguments()
		{
			DoTestCopyToBadArguments(new List<int> { 1, 2, 3, 4 });
		}

		[Test]
		public void ArrayCopyToBadArguments()
		{
			DoTestCopyToBadArguments(new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void CollectionCopyToBadArguments()
		{
			DoTestCopyToBadArguments(new ReadOnlyCollection<int>(new[] { 1, 2, 3, 4 }));
		}

		private static void DoTestCopyTo(IList<int> list)
		{
			Assert.AreEqual(4, list.Count);

			int[] array = new int[4];
			list.CopyTo(0, array, 0, 4);
			CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array);

			array = new int[4];
			list.CopyTo(1, array, 0, 2);
			CollectionAssert.AreEqual(new[] { 2, 3, 0, 0 }, array);

			array = new int[4];
			list.CopyTo(0, array, 1, 2);
			CollectionAssert.AreEqual(new[] { 0, 1, 2, 0 }, array);
		}

		private static void DoTestCopyToBadArguments(IList<int> list)
		{
			Assert.AreEqual(4, list.Count);

			Assert.Throws<ArgumentNullException>(() => list.CopyTo(0, null, 0, 4));

			int[] array = new int[4];
			Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(-1, array, 0, 4));
			Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(0, array, -1, 4));
			Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(0, array, 0, -1));
			Assert.Throws<ArgumentException>(() => list.CopyTo(0, array, 0, 5));
			Assert.Throws<ArgumentException>(() => list.CopyTo(1, array, 0, 4));
			Assert.Throws<ArgumentException>(() => list.CopyTo(0, array, 4, 4));
		}

		[Test]
		public void TransformInPlaceArray()
		{
			int[] an = new int[] { 1, 2, 3, 4 };
			ListUtility.TransformInPlace(an, delegate(int n) { return n * n; });
			CollectionAssert.AreEqual(new int[] { 1, 4, 9, 16 }, an);
		}

		[Test]
		public void TransformInPlaceList()
		{
			List<int> list = new List<int>(new int[] { 1, 2, 3, 4 });
			ListUtility.TransformInPlace(list, delegate(int n) { return n * n; });
			CollectionAssert.AreEqual(new int[] { 1, 4, 9, 16 }, list);
		}

		[Test]
		public void TransformInPlaceNullList()
		{
			Assert.Throws<ArgumentNullException>(() => ListUtility.TransformInPlace<int>(null, delegate(int n) { return n * n; }));
		}

		[Test]
		public void TransformInPlaceNullFunction()
		{
			Assert.Throws<ArgumentNullException>(() => ListUtility.TransformInPlace<int>(new int[] { 1, 2, 3, 4 }, null));
		}

		[Test]
		public void PeekAtOnlyItem()
		{
			Assert.AreEqual(1, ListUtility.Peek(new int[] { 1 }));
		}

		[Test]
		public void PeekAtOneItemOfThree()
		{
			Assert.AreEqual(3, ListUtility.Peek(new int[] { 1, 2, 3 }));
		}

		[Test]
		public void PeekAtEmptyList()
		{
			Assert.Throws<InvalidOperationException>(() => Assert.AreEqual(3, ListUtility.Peek(new int[] { })));
		}

		[Test]
		public void PopOnlyItem()
		{
			List<int> list = new List<int> { 1 };
			Assert.AreEqual(1, ListUtility.Pop(list));
			Assert.AreEqual(0, list.Count);
		}

		[Test]
		public void PopThreeItems()
		{
			List<int> list = new List<int> { 1, 2, 3 };
			Assert.AreEqual(3, ListUtility.Pop(list));
			Assert.AreEqual(2, ListUtility.Pop(list));
			Assert.AreEqual(1, ListUtility.Pop(list));
			Assert.AreEqual(0, list.Count);
		}

		[Test]
		public void PopEmptyList()
		{
			List<int> list = new List<int>();
			Assert.Throws<InvalidOperationException>(() => ListUtility.Peek(list));
		}

		[Test]
		public void PopFromArray()
		{
			Assert.Throws<NotSupportedException>(() => ListUtility.Pop(new int[] { 1 }));
		}

		[Test]
		public void RemoveWhere()
		{
			List<int> list = new List<int> { -2, 3, -5, 1, 5, -10 };
			list.RemoveWhere(x => x < 0);
			CollectionAssert.AreEqual(new[] { 3, 1, 5 }, list);
		}

		[Test]
		public void FindIndexBadArguments()
		{
			Assert.Throws<ArgumentNullException>(delegate { ListUtility.FindIndex<object>(null, n => true); });
			Assert.Throws<ArgumentNullException>(delegate { ListUtility.FindIndex(new object[] { }, null); });
		}

		[Test]
		public void FindIndex()
		{
			Assert.AreEqual(-1, ListUtility.FindIndex(new object[] { }, n => true));
			Assert.AreEqual(0, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, n => n < 5));
			Assert.AreEqual(5, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, n => n > 5));
			Assert.AreEqual(5, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5, n => n > 5));
			Assert.AreEqual(6, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6, n => n > 5));
			Assert.AreEqual(-1, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6, n => n == 5));
			Assert.AreEqual(9, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, n => n == 10));
			Assert.AreEqual(-1, ListUtility.FindIndex(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, n => n == 11));
		}

		static int CompareItemToKey(SearchData bsd, int nKey)
		{
			return bsd.nKey - nKey;
		}

		struct SearchData
		{
			public SearchData(int k, int v)
			{
				nKey = k;
				nValue = v;
			}

			public int nKey;
			public int nValue;
		}
	}
}
