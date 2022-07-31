using System;
using System.IO;

namespace ODFConverter.PDFConverters
{
    internal abstract class BaseConverter
    {
        protected const string ContentFileName = "content.xml";
        protected const string SettingsFileName = "settings.xml";
        protected const string StylesFileName = "styles.xml";
        protected const string MetaFileName = "meta.xml";
        protected const string MediaFolderName = "media";

        public abstract Stream Convert(Stream inputStream);


        public static BaseConverter Create(string extension)
        {
            return extension.ToUpper() switch
            {
                "ODT" => new OdtConverter(),
                "ODS" => new OdsConverter(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
