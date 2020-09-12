# HalfFullWidth

_A .NET Standard library to convert between halfwidth and fullwidth Unicode forms._

## Installation

Install the `HalfFullWidth` package from NuGet.

https://www.nuget.org/packages/HalfFullWidth/

## Usage

Import the `HalfFullWidth` namespace to get access to the extension methods on String and Char:

```cs
using HalfFullWidth;
// ...
string fullwidthString = "ABC".ToFullwidthString(); // ＡＢＣ
string halfwidthString = "ＡＢＣ".ToHalfwidthString(); // ABC
char fullwidthChar = 'A'.ToFullwidthChar(); // Ａ
char halfwidthChar = 'Ａ'.ToHalfwidthChar(); // A
```

Or use the static conversion methods explicitly:

```cs
string fullwidthString = HalfFullWidth.Convert.ToFullwidthString("ABC"); // ＡＢＣ
string halfwidthString = HalfFullWidth.Convert.ToHalfwidthString("ＡＢＣ"); // ABC
char fullwidthChar = HalfFullWidth.Convert.ToFullwidthChar('A'); // Ａ
char halfwidthChar = HalfFullWidth.Convert.ToHalfwidthChar('Ａ'); // A
```

### Supported frameworks

The library targets both .NET Standard 1.3 and .NET Standard 2.0.

## Purpose

This library provides conversion between halfwidth and fullwidth Unicode forms, what is not readily available with .NET
Core. Thus offering a cross-platform alternative to the native Windows APIs (see Alternatives).

Conversion is useful for situations where you need to handle Unicode text that can be represented in both [halfwidth
and fullwidth forms](https://en.wikipedia.org/wiki/Halfwidth_and_fullwidth_forms), commonly used in Chinese, Japanese
and Korean language scripts.

- Chinese: 全形和半形、「fullwidth」的各地常用別名（全角、全形）、「halfwidth」的各地常用別名（半角、半形）
- Japanese: 全角と半角、 半角カナ（はんかくカナ）、半角片仮名（はんかくかたかな, Halfwidth Katakana, 半形假名）
- Korean: 전각 문자와 반각 문자. 전각/반각 모양

For example, in [halfwidth kana (半角カナ)](https://en.wikipedia.org/wiki/Half-width_kana) the below halfwidth Japanese
alphabet would be converted into the below fullwidth representation.

Halfwidth:
```
   ｱ a    ｲ i    ｳ u    ｴ e    ｵ o
K  ｶ ka   ｷ ki   ｸ ku   ｹ ke   ｺ ko   ｷｬ kya  ｷｭ kyu  ｷｮ kyo
S  ｻ sa   ｼ shi  ｽ su   ｾ se   ｿ so   ｼｬ sha  ｼｭ shu  ｼｮ sho
T  ﾀ ta   ﾁ chi  ﾂ tsu  ﾃ te   ﾄ to   ﾁｬ cha  ﾁｭ chu  ﾁｮ cho
N  ﾅ na   ﾆ ni   ﾇ nu   ﾈ ne   ﾉ no   ﾆｬ nya  ﾆｭ nyu  ﾆｮ nyo
H  ﾊ ha   ﾋ hi   ﾌ fu   ﾍ he   ﾎ ho   ﾋｬ hya  ﾋｭ hyu  ﾋｮ hyo
M  ﾏ ma   ﾐ mi   ﾑ mu   ﾒ me   ﾓ mo   ﾐｬ mya  ﾐｭ myu  ﾐｮ myo
Y  ﾔ ya          ﾕ yu          ﾖ yo
R  ﾗ ra   ﾘ ri   ﾙ ru   ﾚ re   ﾛ ro   ﾘｬ rya  ﾘｭ ryu  ﾘｮ ryo
W  ﾜ wa
   ﾝ n
G  ｶﾞ ga   ｷﾞ gi   ｸﾞ gu   ｹﾞ ge   ｺﾞ go   ｷﾞｬ gya  ｷﾞｭ guy  gyo
Z  ｻﾞ za   ｼﾞ ji   ｽﾞ zu   ｾﾞ ze   ｿﾞ zo   ｼﾞｬ ja   ｼﾞｭ ju   ｼﾞｮ jo
D  ﾀﾞ da   ﾁﾞ (ji) ﾂﾞ (zu) ﾃﾞ de   ﾄﾞ do
B  ﾊﾞ ba   ﾋﾞ bi   ﾌﾞ bu   ﾍﾞ be   ﾎﾞ bo   ﾋﾞｬ bya  ﾋﾞｭ byu  byo
P  ﾊﾟ pa   ﾋﾟ pi   ﾌﾟ pu   ﾍﾟ pe   ﾎﾟ po   ﾋﾟｬ pya  ﾋﾟｭ pyu  pyo
F  ﾌｧ fa  ﾌｨ fi         ﾌｪ fe  ﾌｫ fo
T  ﾂｧ tsa ﾃｨ ti  ﾄｩ tu
W                       ｳｪ we  ｳｫ wo";
```

Fullwidth:
```
　　　ア　ａ　　　　イ　ｉ　　　　ウ　ｕ　　　　エ　ｅ　　　　オ　ｏ
Ｋ　　カ　ｋａ　　　キ　ｋｉ　　　ク　ｋｕ　　　ケ　ｋｅ　　　コ　ｋｏ　　　キャ　ｋｙａ　　キュ　ｋｙｕ　　キョ　ｋｙｏ
Ｓ　　サ　ｓａ　　　シ　ｓｈｉ　　ス　ｓｕ　　　セ　ｓｅ　　　ソ　ｓｏ　　　シャ　ｓｈａ　　シュ　ｓｈｕ　　ショ　ｓｈｏ
Ｔ　　タ　ｔａ　　　チ　ｃｈｉ　　ツ　ｔｓｕ　　テ　ｔｅ　　　ト　ｔｏ　　　チャ　ｃｈａ　　チュ　ｃｈｕ　　チョ　ｃｈｏ
Ｎ　　ナ　ｎａ　　　ニ　ｎｉ　　　ヌ　ｎｕ　　　ネ　ｎｅ　　　ノ　ｎｏ　　　ニャ　ｎｙａ　　ニュ　ｎｙｕ　　ニョ　ｎｙｏ
Ｈ　　ハ　ｈａ　　　ヒ　ｈｉ　　　フ　ｆｕ　　　ヘ　ｈｅ　　　ホ　ｈｏ　　　ヒャ　ｈｙａ　　ヒュ　ｈｙｕ　　ヒョ　ｈｙｏ
Ｍ　　マ　ｍａ　　　ミ　ｍｉ　　　ム　ｍｕ　　　メ　ｍｅ　　　モ　ｍｏ　　　ミャ　ｍｙａ　　ミュ　ｍｙｕ　　ミョ　ｍｙｏ
Ｙ　　ヤ　ｙａ　　　　　　　　　　ユ　ｙｕ　　　　　　　　　　ヨ　ｙｏ
Ｒ　　ラ　ｒａ　　　リ　ｒｉ　　　ル　ｒｕ　　　レ　ｒｅ　　　ロ　ｒｏ　　　リャ　ｒｙａ　　リュ　ｒｙｕ　　リョ　ｒｙｏ
Ｗ　　ワ　ｗａ
　　　ン　ｎ
Ｇ　　ガ　ｇａ　　　ギ　ｇｉ　　　グ　ｇｕ　　　ゲ　ｇｅ　　　ゴ　ｇｏ　　　ギャ　ｇｙａ　　ギュ　ｇｕｙ　　ギョ　ｇｙｏ
Ｚ　　ザ　ｚａ　　　ジ　ｊｉ　　　ズ　ｚｕ　　　ゼ　ｚｅ　　　ゾ　ｚｏ　　　ジャ　ｊａ　　　ジュ　ｊｕ　　　ジョ　ｊｏ
Ｄ　　ダ　ｄａ　　　ヂ　（ｊｉ）　ヅ　（ｚｕ）　デ　ｄｅ　　　ド　ｄｏ
Ｂ　　バ　ｂａ　　　ビ　ｂｉ　　　ブ　ｂｕ　　　ベ　ｂｅ　　　ボ　ｂｏ　　　ビャ　ｂｙａ　　ビュ　ｂｙｕ　　ビョ　ｂｙｏ
Ｐ　　パ　ｐａ　　　ピ　ｐｉ　　　プ　ｐｕ　　　ペ　ｐｅ　　　ポ　ｐｏ　　　ピャ　ｐｙａ　　ピュ　ｐｙｕ　　ピョ　ｐｙｏ
Ｆ　　ファ　ｆａ　　フィ　ｆｉ　　　　　　　　　フェ　ｆｅ　　フォ　ｆｏ
Ｔ　　ツァ　ｔｓａ　ティ　ｔｉ　　トゥ　ｔｕ
Ｗ　　　　　　　　　　　　　　　　　　　　　　　ウェ　ｗｅ　　ウォ　ｗｏ
```

## Implementation

The library uses a character mapping based on the code chart of the [Halfwidth and Fullwidth Forms Unicode block
(U+FF00–FFEF)](https://www.unicode.org/charts/PDF/UFF00.pdf) of The Unicode Standard, Version 13.0.

When converting a String from fullwidth into halfwidth, it takes into account characters using variation sequences
(represented as multiple Char in .NET), by removing the variation.

Please note that the library uses [Unicode normalization](http://www.unicode.org/reports/tr15/) to be able to convert to
and from the two representations. Refer to the specific methods for more details.

## Alternatives (Windows only)

There are 2 alternatives for .NET using the Windows API (Win32).

1. P/Invoke to [LCMapString](https://docs.microsoft.com/en-us/windows/win32/api/winnls/nf-winnls-lcmapstringex) and use
   the `LCMAP_FULLWIDTH` and `LCMAP_HALFWIDTH` map flags.
2. Use Visual Basic's [StrConv()](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.strings.strconv)
   function with `VbStrConv.Wide` or `VbStrConv.Narrow` conversion arguments, which uses the above P/Invoke method
   internally, and throws a `PlatformNotSupportedException` when not on Windows.

## Contributing

The source code was developed with the .NET Core 3.1 SDK.

The `.sln` at the root of the project allows to:

- Build the project with `dotnet build`.
- Run unit tests with `dotnet test`.
- Create the NuGet package with `dotnet pack`.

If you find a problem, please feel free to open an issue or submit a PR. Thanks! 🙏
