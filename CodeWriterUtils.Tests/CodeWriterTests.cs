using Xunit;

namespace CodeWriterUtils.Tests
{
    public class CodeWriterTests
    {
        [Fact]
        public void TestWrite()
        {
            const string value = "Hello World";
            var codeWriter = SetupCodeWriter();

            codeWriter.Write(value);
            Assert.Equal(value, codeWriter.ToString());
        }

        [Fact]
        public void TestWriteLine()
        {
            const string value = "Hello World";
            var codeWriter = SetupCodeWriter();

            codeWriter.WriteLine(value);
            Assert.Equal($"{value}\r\n", codeWriter.ToString());
        }

        [Fact]
        public void TestBracketStatement()
        {
            const string value = @"if (0 == 1)
{
    return false;
}
return true;
";

            var codeWriter = SetupCodeWriter();

            using (codeWriter.UseBrackets("if (0 == 1)"))
            {
                codeWriter.WriteLine("return false;");
            }

            codeWriter.WriteLine("return true;");
            Assert.Equal(value, codeWriter.ToString());
        }

        private static CodeWriter SetupCodeWriter()
        {
            return new CodeWriter(new CodeWriterSettings("\r\n"));
        }
    }
}
