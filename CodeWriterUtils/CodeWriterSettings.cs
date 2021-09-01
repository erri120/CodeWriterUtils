using System;
using JetBrains.Annotations;

namespace CodeWriterUtils
{
    [PublicAPI]
    public class CodeWriterSettings
    {
        public readonly int Indentation;
        public readonly string NewlineString;

        public CodeWriterSettings(string? newlineString = null, int indentation = 4)
        {
            Indentation = indentation;
            NewlineString = newlineString ?? Environment.NewLine;
        }
    }
}
