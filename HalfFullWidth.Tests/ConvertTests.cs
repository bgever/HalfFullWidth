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
        [DataRow(
            // "｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ",
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ",
            // "。「」、・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン゛゜",
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A"
        )]
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
        [DataRow(
            // "。「」、・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン゛゜",
            "・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A",
            // "｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ",
            "･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ"
        )]
        public void ToHalfWidthString(string input, string expected)
        {
            string output = Convert.ToHalfWidthString(input);
            Assert.AreEqual(expected, output);
        }
    }
}
