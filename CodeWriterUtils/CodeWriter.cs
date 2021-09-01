using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace CodeWriterUtils
{
    [PublicAPI]
    [DebuggerDisplay("{_sb}")]
    public sealed class CodeWriter
    {
        private readonly StringBuilder _sb;
        private readonly CodeWriterSettings _settings;

        private int _currentIndentationLevel;

        public CodeWriter(CodeWriterSettings? settings = null)
        {
            _sb = new StringBuilder();
            _settings = settings ?? new CodeWriterSettings();
        }

        public void Write(string content)
        {
            _sb.Append(content);
        }

        public void WriteLine(string line)
        {
            WriteIndentation();
            _sb.Append(line);
            WriteNewLine();
        }

        public void WriteLines(params string[] lines)
        {
            foreach (var line in lines)
            {
                WriteLine(line);
            }
        }

        public BracketStatement UseBrackets(string line)
        {
            WriteLine(line);
            return new BracketStatement(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteIndentation()
        {
            _sb.Append(' ', _currentIndentationLevel * _settings.Indentation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void WriteNewLine()
        {
            _sb.Append(_settings.NewlineString);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void IncreaseIndentation(int amount = 1)
        {
            _currentIndentationLevel += amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void DecreaseIndentation(int amount = 1)
        {
            _currentIndentationLevel -= amount;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public sealed class BracketStatement : IDisposable
    {
        private readonly CodeWriter _codeWriter;

        internal BracketStatement(CodeWriter codeWriter)
        {
            _codeWriter = codeWriter;
            codeWriter.WriteLine("{");
            codeWriter.IncreaseIndentation();
        }

        public void Dispose()
        {
            _codeWriter.DecreaseIndentation();
            _codeWriter.WriteLine("}");
        }
    }
}
