using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ODFConverter
{
    public static class ConverterUtils
    {
        public static void ReplaceTags(Stream inputStream, string extension, Dictionary<string, string> variables, ReplaceMode mode = ReplaceMode.AllMatches)
        {
            var replacer = ODFTextReplacer.Create(extension);
            replacer.ReplaceTags(inputStream, variables, mode);
        }

        public static Stream ConvertToPDF(Stream inputStream, string extension)
        {
            var converter = BaseConverter.Create(extension);
            return converter.Convert(inputStream);
        }
    }
}
