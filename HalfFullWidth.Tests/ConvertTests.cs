using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HalfFullWidth.Tests
{
    [TestClass]
    public class ConvertTests
    {
        [TestMethod]
        [DataRow("ﾊﾞｰﾄ ﾊﾞｰﾄ", "バート　バート")] // Bart Bart (space)
        [DataRow("ﾊﾞｰﾄ･ｼﾝﾌﾟｿﾝ", "バート・シンプソン")] // Bart Simpson
        [DataRow("ﾐｽﾀｰｽﾊﾟｰｺﾙ", "ミスタースパーコル")] // Misutā Supākoru
        [DataRow("ﾊﾟﾜｰｸﾘｰﾝ!", "パワークリーン！")] // Power clean! [pawā kurīn!]
        [DataRow("ﾊﾜｰｸﾘｰﾝ!", "ハワークリーン！")] // Hower clean! [hawā kurīn!]
        [DataRow("ﾊﾛｰﾜｰﾙﾄﾞ", "ハローワールド")] // Hello world!
        [DataRow("ｴﾘｯｸ", "エリック")] // Eric
        [DataRow(KATAKANA_ALPHABET_HALF, KATAKANA_ALPHABET_FULL)]
        // --- Next DataRows are based on https://www.unicode.org/charts/PDF/UFF00.pdf ---
        [DataRow(" ", "　")] // Ideographic space.
        [DataRow("\u0020", "\u3000")] // Ideographic space.
        [DataRow(
            "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~",
            "！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～"
        )] // Fullwidth ASCII variants
        [DataRow("⦅⦆", "｟｠")] // Fullwidth brackets
        [DataRow("｡｢｣､", "。「」、")] // Halfwidth CJK punctuation
        [DataRow(
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ",
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A"
        )] // Halfwidth Katakana variants
        public void ToFullWidthString(string input, string expected)
        {
            string output = input.ToFullWidthString();
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        [DataRow("バート　バート", "ﾊﾞｰﾄ ﾊﾞｰﾄ")] // Bart Bart (space)
        [DataRow("バート・シンプソン", "ﾊﾞｰﾄ･ｼﾝﾌﾟｿﾝ")] // Bart Simpson
        [DataRow("ミスタースパーコル", "ﾐｽﾀｰｽﾊﾟｰｺﾙ")] // Misutā Supākoru
        [DataRow("パワークリーン！", "ﾊﾟﾜｰｸﾘｰﾝ!")] // Power clean! [pawā kurīn!]
        [DataRow("ハワークリーン！", "ﾊﾜｰｸﾘｰﾝ!")] // Hower clean! [hawā kurīn!]
        // [DataRow("ハワークリーン！︀", "ﾊﾜｰｸﾘｰﾝ!")] // Hower clean! [hawā kurīn!] with corner-justified form
        // [DataRow("ハワークリーン！︁", "ﾊﾜｰｸﾘｰﾝ!")] // Hower clean! [hawā kurīn!] with centered form
        [DataRow("ハローワールド", "ﾊﾛｰﾜｰﾙﾄﾞ")] // Hello world!
        [DataRow("エリック", "ｴﾘｯｸ")] // Eric
        [DataRow(KATAKANA_ALPHABET_FULL, KATAKANA_ALPHABET_HALF)]
        // --- Next DataRows are based on https://www.unicode.org/charts/PDF/UFF00.pdf ---
        [DataRow("　", " ")] // Ideographic space.
        [DataRow("\u3000", "\u0020")] // Ideographic space.
        [DataRow(
            "！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～",
            "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~"
        )] // Fullwidth ASCII variants
        [DataRow("｟｠", "⦅⦆")] // Fullwidth brackets
        [DataRow("。「」、", "｡｢｣､")] // Halfwidth CJK punctuation
        [DataRow(
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A",
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ"
        )] // Halfwidth Katakana variants
        public void ToHalfWidthString(string input, string expected)
        {
            string output = Convert.ToHalfWidthString(input);
            Assert.AreEqual(expected, output);
        }

        const string MIXED = "THE ｑｕｉｃｋ， ＢＲＯＷＮ\u3000fox.";
        const string MIXED_TO_FULL = "ＴＨＥ\u3000ｑｕｉｃｋ，\u3000ＢＲＯＷＮ\u3000ｆｏｘ．";
        const string MIXED_TO_HALF = "THE quick, BROWN fox.";

        [TestMethod]
        public void ToFullWidthString_Mixed()
        {
            string output = MIXED.ToFullWidthString();
            Assert.AreEqual(MIXED_TO_FULL, output);
        }

        [TestMethod]
        public void ToHalfWidthString_Mixed()
        {
            string output = MIXED.ToHalfWidthString();
            Assert.AreEqual(MIXED_TO_HALF, output);
        }

        const string KATAKANA_ALPHABET_HALF = @"
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
            G  ｶﾞ ga   ｷﾞ gi   ｸﾞ gu   ｹﾞ ge   ｺﾞ go   ｷﾞｬ gya  ｷﾞｭ guy  ｷﾞｮ gyo
            Z  ｻﾞ za   ｼﾞ ji   ｽﾞ zu   ｾﾞ ze   ｿﾞ zo   ｼﾞｬ ja   ｼﾞｭ ju   ｼﾞｮ jo
            D  ﾀﾞ da   ﾁﾞ (ji) ﾂﾞ (zu) ﾃﾞ de   ﾄﾞ do
            B  ﾊﾞ ba   ﾋﾞ bi   ﾌﾞ bu   ﾍﾞ be   ﾎﾞ bo   ﾋﾞｬ bya  ﾋﾞｭ byu  ﾋﾞｮ byo
            P  ﾊﾟ pa   ﾋﾟ pi   ﾌﾟ pu   ﾍﾟ pe   ﾎﾟ po   ﾋﾟｬ pya  ﾋﾟｭ pyu  ﾋﾟｮ pyo
            F  ﾌｧ fa  ﾌｨ fi         ﾌｪ fe  ﾌｫ fo
            T  ﾂｧ tsa ﾃｨ ti  ﾄｩ tu
            W                       ｳｪ we  ｳｫ wo";

        const string KATAKANA_ALPHABET_FULL = @"
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
　　　　　　　　　　　　Ｗ　　　　　　　　　　　　　　　　　　　　　　　ウェ　ｗｅ　　ウォ　ｗｏ";
    }
}
