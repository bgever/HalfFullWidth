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
        [DataRow("｡｢｣､", "。「」、")] // Halfwidth CJK punctuation
        [DataRow("\u0020", "\u3000")] // Ideographic space.
        [DataRow(
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ",
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A"
        )] // Halfwidth Katakana variants
        [DataRow(
            "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~",
            "！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～"
        )] // Fullwidth ASCII variants
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
        // --- Next DataRows are based on https://www.unicode.org/charts/PDF/UFF00.pdf ---
        [DataRow("。「」、", "｡｢｣､")] // Halfwidth CJK punctuation
        [DataRow("\u3000", "\u0020")] // Ideographic space.
        [DataRow(
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A",
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ"
        )] // Halfwidth Katakana variants
        [DataRow(
            "！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～",
            "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~"
        )] // Fullwidth ASCII variants
        public void ToHalfWidthString(string input, string expected)
        {
            string output = Convert.ToHalfWidthString(input);
            Assert.AreEqual(expected, output);
        }

        const string MIXED = "THE ｑｕｉｃｋ， ＢＲＯＷＮ\u3000fox.";
        const string MIXED_TO_FULL = "ＴＨＥ\u3000ｑｕｉｃｋ，\u3000ＢＲＯＷＮ\u3000ｆｏｘ．";
        const string MIXED_TO_HALF = "THE quick, BROWN fox.";

        public void ToFullWidthString_Mixed()
        {
            string output = MIXED.ToFullWidthString();
            Assert.AreEqual(MIXED_TO_FULL, output);
        }

        public void ToHalfWidthString_Mixed()
        {
            string output = MIXED.ToHalfWidthString();
            Assert.AreEqual(MIXED_TO_HALF, output);
        }
    }
}
