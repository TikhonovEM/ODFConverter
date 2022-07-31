using System;
using System.Collections.Generic;
using System.IO;

namespace ODFConverter.TagsReplacers
{
    internal abstract class OdfTextReplacer
    {
        public abstract void ReplaceTags(Stream inputStream, Dictionary<string, string> variables, ReplaceMode mode);
        public static OdfTextReplacer Create(string extension)
        {
            return extension.ToUpper() switch
            {
                "ODT" => new OdtTextReplacer(),
                _ => throw new ArgumentException($"Unknown extension {extension}")
            };
        }
    }
}
