using System;
using System.Collections.Generic;
using System.Linq;

namespace Faithlife.Utility
{
	/// <summary>
	/// Methods for working with IHasEquivalence.
	/// </summary>
	public static class Equivalence
	{
		/// <summary>
		/// True if the objects are equivalent.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <param name="left">The left object.</param>
		/// <param name="right">The right object.</param>
		/// <returns>True if the objects are equivalent.</returns>
		public static bool AreEquivalent<T>(T left, T right)
			where T : IHasEquivalence<T>
		{
			return left == null ? right == null : left.IsEquivalentTo(right);
		}

		/// <summary>
		/// True if the sequences are equivalent.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <param name="left">The left sequence.</param>
		/// <param name="right">The right sequence.</param>
		/// <returns>True if the sequence are equivalent.</returns>
		public static bool AreSequencesEquivalent<T>(IEnumerable<T> left, IEnumerable<T> right)
			where T : IHasEquivalence<T>
		{
			return left == null ? right == null : right != null && left.SequenceEquivalent(right);
		}

		/// <summary>
		/// Returns an equality comparer that calls IHasEquivalence.IsEquivalentTo.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <returns>The equality comparer.</returns>
		public static IEqualityComparer<T> GetEqualityComparer<T>()
			where T : IHasEquivalence<T>
		{
			return new EquivalenceComparer<T, T>();
		}

		/// <summary>
		/// Returns an equality comparer that calls IHasEquivalence.IsEquivalentTo.
		/// </summary>
		/// <typeparam name="TDerived">The object type.</typeparam>
		/// <typeparam name="TBase">The base type that implements IHasEquivalence.</typeparam>
		/// <returns>The equality comparer.</returns>
		public static IEqualityComparer<TDerived> GetEqualityComparer<TDerived, TBase>()
			where TDerived : TBase, IHasEquivalence<TBase>
		{
			return new EquivalenceComparer<TDerived, TBase>();
		}

		/// <summary>
		/// Returns an equality comparer that calls IHasEquivalence.IsEquivalentTo.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <returns>The equality comparer.</returns>
		/// <remarks>If T does not implement IHasEquivalence{T}, this method returns
		/// EqualityComparer{T}.Default as a fallback instead.</remarks>
		public static IEqualityComparer<T> GetEqualityComparerOrFallback<T>()
		{
			return GetEqualityComparerOrFallback(EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Returns an equality comparer that calls IHasEquivalence.IsEquivalentTo.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <returns>The equality comparer.</returns>
		/// <remarks>If T does not implement IHasEquivalence{T}, this method returns
		/// the specified fallback instead.</remarks>
		public static IEqualityComparer<T> GetEqualityComparerOrFallback<T>(IEqualityComparer<T> fallback)
		{
			return EquivalenceComparerCache<T>.Instance ?? fallback;
		}

		/// <summary>
		/// True if the sequences are equivalent.
		/// </summary>
		/// <typeparam name="T">The object type.</typeparam>
		/// <param name="seq">The sequence.</param>
		/// <param name="other">The other sequence.</param>
		/// <returns>True if the sequences are equivalent.</returns>
		public static bool SequenceEquivalent<T>(this IEnumerable<T> seq, IEnumerable<T> other)
			where T : IHasEquivalence<T>
		{
			return seq.SequenceEqual(other, GetEqualityComparer<T>());
		}

		private static class EquivalenceComparerCache<T>
		{
			public static readonly IEqualityComparer<T> Instance = CreateInstance();

			private static IEqualityComparer<T> CreateInstance()
			{
				Type type = typeof(T);
				do
				{
					if (typeof(IHasEquivalence<>).MakeGenericType(type).IsAssignableFrom(typeof(T)))
						return (IEqualityComparer<T>) Activator.CreateInstance(typeof(EquivalenceComparer<,>).MakeGenericType(typeof(T), type));
					type = type.GetBaseType();
				} while (type != null);

				return null;
			}
		}

		private sealed class EquivalenceComparer<TDerived, TBase> : EqualityComparer<TDerived>
			where TDerived : TBase, IHasEquivalence<TBase>
		{
			public override bool Equals(TDerived left, TDerived right)
			{
				return left == null ? right == null : left.IsEquivalentTo(right);
			}

			public override int GetHashCode(TDerived value)
			{
				throw new NotImplementedException();
			}
		}
	}
}
