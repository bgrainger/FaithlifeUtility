# EventInfo&lt;TSource,TEventHandler&gt; constructor

Initializes a new instance of the [`EventInfo`](../EventInfo-2.md) class.

```csharp
public EventInfo(Action<TSource, TEventHandler> fnAddHandler, Action<TSource, TEventHandler> fnRemoveHandler)
```

| parameter | description |
| --- | --- |
| fnAddHandler | A delegate that adds an event handler to the event of the specified source. |
| fnRemoveHandler | A delegate that removes an event handler from the event of the specified source. |

## See Also

* class [EventInfo&lt;TSource,TEventHandler&gt;](../EventInfo-2.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
