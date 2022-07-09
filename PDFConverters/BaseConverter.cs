using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ODFConverter
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
                "ODT" => new ODTConverter(),
                "ODS" => new ODSConverter(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
