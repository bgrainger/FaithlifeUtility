# EnumerableUtility.Downcast&lt;TSource,TDest&gt; method

Enumerates the specified collection, casting each element to a derived type.

```csharp
public static IEnumerable<TDest> Downcast<TSource, TDest>(this IEnumerable<TSource> seq)
    where TDest : TSource
```

| parameter | description |
| --- | --- |
| seq | The collection to enumerate. |

## Return Value

The elements of the specified collection, cast to the destination type.

## Exceptions

| exception | condition |
| --- | --- |
| InvalidCastException | One of the elements could not be cast to the derived type. |

## Remarks

This method can only be used when the destination type is derived from the source type.

## See Also

* method [Upcast&lt;TSource,TDest&gt;](Upcast.md)
* class [EnumerableUtility](../EnumerableUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
