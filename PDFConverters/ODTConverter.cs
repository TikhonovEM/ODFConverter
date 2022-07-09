using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ODFConverter
{
    internal class ODTConverter : BaseConverter
    {
        public override Stream Convert(Stream inputStream)
        {
            using var pdfStream = new MemoryStream();

            using var archive = new ZipArchive(inputStream, ZipArchiveMode.Read);

            var pdfBuilder = DocumentBuilder.New();
            var sectionBuilder = pdfBuilder.AddSection()
                .SetMargins(0, 0, 0, 0)
                .SetSize(PaperSize.A4);



            pdfBuilder.Build(pdfStream);
            return pdfStream;
        }
    }
}
