using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HalfFullWidth
{
    /// <summary>
    /// Unicode conversions between halfwidth and fullwidth forms.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <description>Normalization forms:
    /// <seealso cref="http://www.unicode.org/reports/tr15/" /></description>
    /// </item>
    /// <item>
    /// <description>Halfwidth Kana:
    /// <seealso cref="https://en.wikipedia.org/wiki/Half-width_kana" /></description>
    /// </item>
    /// <item>
    /// <description>Variation sequences for punctuation alignment:
    /// <seealso cref="https://en.wikipedia.org/wiki/Halfwidth_and_Fullwidth_Forms_(Unicode_block)" /></description>
    /// </item>
    /// </list>
    /// Inspiration:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// https://stackoverflow.com/questions/57142760/check-if-a-string-is-half-width-or-full-width-in-c-sharp
    /// </description>
    /// </item>
    /// <item>
    /// <description>https://github.com/sampathsris/ascii-fullwidth-halfwidth-convert</description>
    /// </item>
    /// </list>
    /// </remarks>
    public static class Convert
    {
        /// <summary>
        /// Converts halfwidth <see cref="Char" /> into their fullwidth <see cref="Char" /> form, based on the mapping
        /// as defined in the Halfwidth and Fullwidth Forms Unicode block (U+FF00–FFEF).
        /// </summary>
        /// <remarks>
        /// Note that this conversion cannot support fullwidth forms that need to be represented as a variation sequence
        /// in a <see cref="String" />.
        /// </remarks>
        /// <param name="input">Any <see cref="Char" /> to be converted. Only halfwidth <see cref="Char" /> are
        /// converted into fullwidth, other <see cref="Char" /> are returned as-is.</param>
        /// <returns>The matching fullwidth form, or else the unchanged <see cref="Char" />.</returns>
        public static char ToFullwidthChar(this char input)
        {
            return Mapping.Value.TryGetValue(input, out char converted) ? converted : input;
        }

        /// <summary>
        /// Converts fullwidth <see cref="Char" /> into their halfwidth <see cref="Char" /> form, based on the mapping
        /// as defined in the Halfwidth and Fullwidth Forms Unicode block (U+FF00–FFEF).
        /// </summary>
        /// <remarks>
        /// Note that this conversion cannot support fullwidth forms that need to be represented as a variation sequence
        /// in a <see cref="String" />.
        /// </remarks>
        /// <param name="input">Any <see cref="Char" /> to be converted. Only fullwidth <see cref="Char" /> are
        /// converted into halfwidth, other <see cref="Char" /> are returned as-is.</param>
        /// <returns>The matching halfwidth form, or else the unchanged <see cref="Char" />.</returns>
        public static char ToHalfwidthChar(this char input)
        {
            return ReverseMapping.Value.TryGetValue(input, out char converted) ? converted : input;
        }

        /// <summary>
        /// Converts any halfwidth <see cref="Char" /> in the <see cref="String" /> into their fullwidth 
        /// <see cref="Char" /> form, based on the mapping as defined in the Halfwidth and Fullwidth Forms Unicode block
        /// (U+FF00–FFEF). The string is returned in Normalization Form C.
        /// </summary>
        /// <remarks>
        /// Note that this conversion cannot support fullwidth forms that need to be represented as a variation sequence
        /// in a <see cref="String" />.
        /// Note that the string can become shorter due to normalization.
        /// </remarks>
        /// <param name="input">The <see cref="String" /> containing halfwidth <see cref="Char" /> to be converted. Only
        /// halfwidth <see cref="Char" /> are converted into fullwidth, other <see cref="Char" /> in the
        /// <see cref="String" /> are left as-is.</param>
        /// <returns>
        /// The converted string normalized to Normalization Form C, with halfwidth <see cref="Char" /> converted into
        /// their fullwidth form.
        /// </returns>
        public static string ToFullwidthString(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var sb = new StringBuilder(input.Length);
            foreach (char c in input) sb.Append(c.ToFullwidthChar());
            // Return the string in Normalization Form C, to combine characters.
            return sb.ToString().Normalize();
        }

        /// <summary>
        /// Converts any fullwidth <see cref="Char" /> in the <see cref="String" /> into their halfwidth
        /// <see cref="Char" /> form, based on the mapping as defined in the Halfwidth and Fullwidth Forms Unicode block
        /// (U+FF00–FFEF). The string is returned in Normalization Form D.
        /// </summary>
        /// <remarks>
        /// Note that this conversion supports fullwidth forms that are represented with a variation sequence in a
        /// <see cref="String" /> (multiple <see cref="Char" />), but that this conversion is not bi-directional and
        /// that the variation sequence will be lost when converting back from halfwidth to fullwidth.
        /// Note that the string can become longer due to normalization.
        /// </remarks>
        /// <param name="input">The <see cref="String" /> containing fullwidth <see cref="Char" /> to be converted. Only
        /// fullwidth <see cref="Char" /> are converted into halfwidth, other <see cref="Char" /> in the
        /// <see cref="String" /> are left as-is.</param>
        /// <returns>
        /// The converted string normalized to Normalization Form D, with fullwidth <see cref="Char" /> converted into
        /// their halfwidth form.
        /// </returns>
        public static string ToHalfwidthString(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            foreach (var variantMapping in VariationSequenceMappings.Value)
            {
                input = input.Replace(variantMapping.Key, variantMapping.Value.ToString());
            }
            // Normalize the string to form D, to separate characters.
            input = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(input.Length);
            foreach (char c in input) sb.Append(c.ToHalfwidthChar());
            return sb.ToString();
        }

        /// <summary>
        /// Lazy loaded mapping which has as dictionary key the fullwidth Char, and as value a halfwidth representation.
        /// (the reverse of <see cref="Mapping">)
        /// </summary>
        /// <returns>Lazy loaded mapping table.</returns>
        static Lazy<Dictionary<char, char>> ReverseMapping = new Lazy<Dictionary<char, char>>(
            () => Mapping.Value
                    .Select(pair => (Full: pair.Value, Half: pair.Key))
                    .ToDictionary(x => x.Full, x => x.Half)
        );

        /// <summary>
        /// Lazy loaded mapping which has as dictionary key the halfwidth Char, and as value a list of fullwidth
        /// representations, with at the first entry the preferred fullwidth Char.
        /// </summary>
        /// <returns>Lazy loaded mapping table.</returns>
        static Lazy<Dictionary<char, char>> Mapping = new Lazy<Dictionary<char, char>>(
            () => new Dictionary<char, char> {
                // Ideographic space
                {' ', '　'}, // ASCII 0020
                // Fullwidth brackets
                {'⦅', '｟'},
                {'⦆', '｠'},
                // Halfwidth CJK punctuation — See CJK punctuation 3000-303F
                {'｡', '。'},
                {'｢', '「'},
                {'｣', '」'},
                {'､', '、'},
                // Halfwidth Katakana variants — See Katakana 30A0-30FF
                {'･', '・'},
                {'ｦ', 'ヲ'},
                {'ｧ', 'ァ'},
                {'ｨ', 'ィ'},
                {'ｩ', 'ゥ'},
                {'ｪ', 'ェ'},
                {'ｫ', 'ォ'},
                {'ｬ', 'ャ'},
                {'ｭ', 'ュ'},
                {'ｮ', 'ョ'},
                {'ｯ', 'ッ'},
                {'ｰ', 'ー'},
                {'ｱ', 'ア'},
                {'ｲ', 'イ'},
                {'ｳ', 'ウ'},
                {'ｴ', 'エ'},
                {'ｵ', 'オ'},
                {'ｶ', 'カ'},
                {'ｷ', 'キ'},
                {'ｸ', 'ク'},
                {'ｹ', 'ケ'},
                {'ｺ', 'コ'},
                {'ｻ', 'サ'},
                {'ｼ', 'シ'},
                {'ｽ', 'ス'},
                {'ｾ', 'セ'},
                {'ｿ', 'ソ'},
                {'ﾀ', 'タ'},
                {'ﾁ', 'チ'},
                {'ﾂ', 'ツ'},
                {'ﾃ', 'テ'},
                {'ﾄ', 'ト'},
                {'ﾅ', 'ナ'},
                {'ﾆ', 'ニ'},
                {'ﾇ', 'ヌ'},
                {'ﾈ', 'ネ'},
                {'ﾉ', 'ノ'},
                {'ﾊ', 'ハ'},
                {'ﾋ', 'ヒ'},
                {'ﾌ', 'フ'},
                {'ﾍ', 'ヘ'},
                {'ﾎ', 'ホ'},
                {'ﾏ', 'マ'},
                {'ﾐ', 'ミ'},
                {'ﾑ', 'ム'},
                {'ﾒ', 'メ'},
                {'ﾓ', 'モ'},
                {'ﾔ', 'ヤ'},
                {'ﾕ', 'ユ'},
                {'ﾖ', 'ヨ'},
                {'ﾗ', 'ラ'},
                {'ﾘ', 'リ'},
                {'ﾙ', 'ル'},
                {'ﾚ', 'レ'},
                {'ﾛ', 'ロ'},
                {'ﾜ', 'ワ'},
                {'ﾝ', 'ン'},
                {'ﾞ', '\u3099'}, // (゛) KATAKANA VOICED SOUND MARK
                {'ﾟ', '\u309A'}, // (゜) KATAKANA SEMI-VOICED SOUND MARK
                // Halfwidth Hangul variants — See Hangul Compatibility Jamo 3130-318F
                {'\uFFA0', '\u3164'}, // HALFWIDTH HANGUL FILLER
                // Fullwidth symbol variants — See Latin-1 00A0-00FF
                {'¢', '￠'},
                {'£', '￡'},
                {'¬', '￢'},
                {'¯', '￣'},
                {'¦', '￤'},
                {'¥', '￥'},
                {'₩', '￦'},
                // Halfwidth symbol variants
                {'￨', '│'},
                {'￩', '←'},
                {'￪', '↑'},
                {'￫', '→'},
                {'￬', '↓'},
                {'￭', '■'},
                {'￮', '○'},
            }
            // Fullwidth ASCII variants — See ASCII 0020-007E
            .Concat(new CharRange('\u0021', '\u007E').Map(new CharRange('\uFF01', '\uFF5E')))
            // Halfwidth Hangul variants — See Hangul Compatibility Jamo 3130-318F
            .Concat(new CharRange('\uFFA1', '\uFFBE').Map(new CharRange('\u3131', '\u314E')))
            .Concat(new CharRange('\uFFC2', '\uFFC7').Map(new CharRange('\u314F', '\u3154')))
            .Concat(new CharRange('\uFFCA', '\uFFCF').Map(new CharRange('\u3155', '\u315A')))
            .Concat(new CharRange('\uFFD2', '\uFFD7').Map(new CharRange('\u315B', '\u3160')))
            .Concat(new CharRange('\uFFDA', '\uFFDC').Map(new CharRange('\u3161', '\u3163')))
            .ToDictionary(x => x.Key, x => x.Value)
        );

        /// <summary>
        /// Lazy loaded mappings for variation sequences of specific fullwidth Char's.
        /// </summary>
        /// <remarks>Sequences must be stored in a String since a Char cannot hold the sequence.</remarks>
        /// <returns>
        /// A mapping dictionary optimized for lookup, where different unique keys may point to the same fullwidth Char.
        /// </returns>
        static Lazy<Dictionary<string, char>> VariationSequenceMappings = new Lazy<Dictionary<string, char>>(
            () => new Dictionary<char, string[]> {
                // Variations for punctuation alignment. ([0] = corner-justified form, [1] = centered form)
                {'！', new string[] { "！︀", "！︁" }},
                {'，', new string[] { "，︀", "，︁" }},
                {'．', new string[] { "．︀", "．︁" }},
                {'：', new string[] { "：︀", "：︁" }},
                {'；', new string[] { "；︀", "；︁" }},
                {'？', new string[] { "？︀", "？︁" }},
                // Variations for full width zero.
                {'０', new string[] { "０︀" }},
            }
            // Expand the code optimized mapping into a lookup mapping.
            .SelectMany(x => x.Value.Select(v => (Sequence: v, Char: x.Key)))
            .ToDictionary(x => x.Sequence, x => x.Char)
        );
    }
}
