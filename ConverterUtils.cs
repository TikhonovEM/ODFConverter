using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ODFConverter.PDFConverters;
using ODFConverter.TagsReplacers;

namespace ODFConverter
{
    public static class ConverterUtils
    {
        public static void ReplaceTags(Stream inputStream, string extension, Dictionary<string, string> variables, ReplaceMode mode = ReplaceMode.AllMatches)
        {
            var replacer = OdfTextReplacer.Create(extension);
            replacer.ReplaceTags(inputStream, variables, mode);
        }

        public static Stream ConvertToPdf(Stream inputStream, string extension)
        {
            var converter = BaseConverter.Create(extension);
            return converter.Convert(inputStream);
        }
    }
}
