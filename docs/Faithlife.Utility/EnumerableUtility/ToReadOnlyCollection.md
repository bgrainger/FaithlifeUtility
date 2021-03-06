# EnumerableUtility.ToReadOnlyCollection&lt;T&gt; method

Represents the sequence as a ReadOnlyCollection.

```csharp
public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> seq)
```

| parameter | description |
| --- | --- |
| T | The type of the element. |
| seq | The sequence. |

## Return Value

An ReadOnlyCollection containing the items in the sequence.

## Remarks

If the sequence is an IList, a ReadOnlyCollection is created to wrap it. Otherwise, the sequence is copied into a IList and then wrapped in a ReadOnlyCollection. This method is useful for forcing evaluation of a potentially lazy sequence while retaining reasonable performance for sequences that are already anIList.

## See Also

* class [EnumerableUtility](../EnumerableUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
