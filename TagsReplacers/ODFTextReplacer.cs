using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ODFConverter
{
    internal abstract class ODFTextReplacer
    {
        public abstract void ReplaceTags(Stream inputStream, Dictionary<string, string> variables, ReplaceMode mode);
        public static ODFTextReplacer Create(string extension)
        {
            return extension.ToUpper() switch
            {
                "ODT" => new ODTTextReplacer(),
                _ => throw new ArgumentException($"Unknown extension {extension}")
            };
        }
    }
}
