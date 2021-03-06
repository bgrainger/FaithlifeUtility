# EnumUtility.TryParse&lt;T&gt; method (1 of 4)

Attempts to parse the specified string.

```csharp
public static T? TryParse<T>(string value)
    where T : struct
```

| parameter | description |
| --- | --- |
| T | The enumerated value type. |
| value | The string. |

## Return Value

A strongly typed enumerated value; or null if the string could not be successfully parsed.

## Remarks

This method matches case.

## See Also

* class [EnumUtility](../EnumUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

---

# EnumUtility.TryParse&lt;T&gt; method (2 of 4)

Attempts to parse the specified string.

```csharp
public static T? TryParse<T>(string value, CaseSensitivity caseSensitivity)
    where T : struct
```

| parameter | description |
| --- | --- |
| T | The enumerated value type. |
| value | The string. |
| caseSensitivity | The case sensitivity. |

## Return Value

A strongly typed enumerated value; null if the string could not be successfully parsed.

## See Also

* enum [CaseSensitivity](../CaseSensitivity.md)
* class [EnumUtility](../EnumUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

---

# EnumUtility.TryParse&lt;T&gt; method (3 of 4)

```csharp
public static bool TryParse<T>(string value, out T result)
    where T : struct
```

## See Also

* class [EnumUtility](../EnumUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

---

# EnumUtility.TryParse&lt;T&gt; method (4 of 4)

```csharp
public static bool TryParse<T>(string value, CaseSensitivity caseSensitivity, out T result)
    where T : struct
```

## See Also

* enum [CaseSensitivity](../CaseSensitivity.md)
* class [EnumUtility](../EnumUtility.md)
* namespace [Faithlife.Utility](../../Faithlife.Utility.md)

<!-- DO NOT EDIT: generated by xmldocmd for Faithlife.Utility.dll -->
