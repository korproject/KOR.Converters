# KOR.Converters
Included custom useful converters for WPF projects


## Classes And Common Usages

XAML namespace

```xaml
xmlns:converters="clr-namespace:KOR.Converters;assembly=KOR.Converters"
```
and define locally in resource area or global in app.xaml like as

```xaml
<converters:xconverter x:Key="xconverter"/>
```

C# namespace
```csharp
using KOR.Converters;
```
</b>
</b>

### Bool2VisibilityConverter

```xaml
...
<converters:Bool2VisibilityConverter x:Key="Bool2VisibilityConverter"/>

...
... Visibility="{Binding MyVisibilityBool, Converter={StaticResource Bool2VisibilityConverter}, ConverterParameter=toggle-collapsed}" ...
```

**Supported Converter Parameters**
- **null**: default option, returns false=>Hidden, true=>Visible
- **default-hidden**: false=>Hidden, true=>Visible
- **default-collapsed**: false=>Collapsed, true=>Visible
- **toggle-hidden**: false=>Visible, true=>Hidden
- **toggle-collapsed**: false=>Visible, true=>Collapsed
</b>
</b>

### StringLimitConverter

```xaml
...
<converters:StringLimitConverter x:Key="StringLimitConverter"/>

...
... Text="{Binding Header, Converter={StaticResource StringLimitConverter}, ConverterParameter=word-3}" ...
```

**Supported Converter Parameters**
- **null**: default option, returns sended value
- **char-length**: string.SubString(0, length)
- **char-length-suffix**: string.SubString(0, length) + "{suffix}"
- **word-length**: LimitWords(string, length)
- **word-length-suffix**: LimitWords(string, length, suffix)

*LimitWords* is not native function

**Examples**
- **char-5**: sended value: 0123456789, return value: 01234
- **char-5-...** sended value: 0123456789, return value: 01234...
- **word-3**: sended value: I can not o this, return value: I can not
- **word-3-...**: sended value: I can not o this, return value: I can not...

**Caution**: LimitWords return only words, for example  
Parameter: word-5-...  
Sended value: "This examples depends/up to your imaginations"  
Return value: "This examples depends up to..." (there is not slash / anymore)
</b>
</b>