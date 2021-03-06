# UriUtility.ParseWebUrl method

Parses the text as a web URI.

```csharp
public static Uri ParseWebUrl(string uriText)
```

| parameter | description |
| --- | --- |
| uriText | The URI text. |

## Return Value

A web URI, or `null`.

## Remarks

This does not guarantee the URI contains a valid hostname, or refers to a valid resource on that server; it simply determines if it can create a valid URI beginning with `http://` or `https://`.

## See Also

* class [UriUtility](../UriUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
