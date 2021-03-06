# WorkState class

Standard implementations of [`IWorkState`](IWorkState.md).

```csharp
public static class WorkState
```

## Public Members

| name | description |
| --- | --- |
| static [None](WorkState/None.md) { get; } | A non-cancellable work state that can be used when the caller does not want to support work cancellation. |
| static [ToDo](WorkState/ToDo.md) { get; } | This use of [`WorkState`](WorkState.md) needs to be investigated and replaced with either a real [`IWorkState`](IWorkState.md) or [`None`](WorkState/None.md) as appropriate. |
| static [FromCancellationToken](WorkState/FromCancellationToken.md)(…) | Returns an [`IWorkState`](IWorkState.md) that will be canceled when the specified CancellationToken is canceled. |

## See Also

* namespace [Faithlife.Utility.Threading](../Faithlife.Utility.md)
* [WorkState.cs](https://github.com/Faithlife/FaithlifeUtility/tree/master/src/Faithlife.Utility/Threading/WorkState.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
