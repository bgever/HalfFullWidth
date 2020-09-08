using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HalfFullWidth
{
    /// <summary>
    /// Half-width table: https://en.wikipedia.org/wiki/Half-width_kana
    /// Normalization forms: http://www.unicode.org/reports/tr15/
    /// Variation sequences for punctuation alignment: https://en.wikipedia.org/wiki/Halfwidth_and_Fullwidth_Forms_(Unicode_block)
    /// Punctuation: https://en.wikipedia.org/wiki/CJK_Symbols_and_Punctuation
    /// 
    /// Inspiration:
    /// - https://stackoverflow.com/questions/57142760/check-if-a-string-is-half-width-or-full-width-in-c-sharp
    /// - https://github.com/sampathsris/ascii-fullwidth-halfwidth-convert
    /// </summary>
    public static class Convert
    {
        public static char ToFullWidthChar(this char input)
        {
            return Mapping.Value.TryGetValue(input, out char converted) ? converted : input;
        }

        public static char ToHalfWidthChar(this char input)
        {
            return ReverseMapping.Value.TryGetValue(input, out char converted) ? converted : input;
        }

        public static string ToFullWidthString(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            var sb = new StringBuilder(input.Length);
            foreach (char c in input) sb.Append(c.ToFullWidthChar());
            // Return the string in Normalization Form C, to combine characters.
            return sb.ToString().Normalize();
        }

        public static string ToHalfWidthString(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            // Normalize the string to form D, to separate characters.
            input = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(input.Length);
            foreach (char c in input) sb.Append(c.ToHalfWidthChar());
            return sb.ToString();
        }

        static Lazy<Dictionary<char, char>> ReverseMapping = new Lazy<Dictionary<char, char>>(
            () => Mapping.Value
                    .Select(pair => (Full: pair.Value, Half: pair.Key))
                    .ToDictionary(x => x.Full, x => x.Half)
        );

        /// <summary>
        /// Lazy loaded mapping which has as dictionary key the half-width character, and as value a list of full-width
        /// representations, with at the first entry the preferred full-width character.
        /// </summary>
        /// <returns>Lazy loaded mapping table.</returns>
        static Lazy<Dictionary<char, char>> Mapping = new Lazy<Dictionary<char, char>>(
            () => new Dictionary<char, char>() {
                {' ', '　'}, // Ideographic space
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
            }
            // Fullwidth ASCII variants — See ASCII 0020-007E
            .Concat(new CharRange('\u0021', '\u007E').Map(new CharRange('\uFF01', '\uFF5E')))
            .ToDictionary(x => x.Key, x => x.Value)
        );
    }
}
