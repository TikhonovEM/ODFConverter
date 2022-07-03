using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Linq;

namespace ODFConverter
{
    public static class ConverterUtils
    {
        public static void ReplaceTags(Stream inputStream, Dictionary<string, string> variables)
        {
            var magazine = variables.ToDictionary(k => k.Key, v => v.Value);
            
            using var archive = new ZipArchive(inputStream, ZipArchiveMode.Update);
            var entry = archive.GetEntry("content.xml");

            // XmlWriter будем вести запись в StringBuilder
            var sb = new StringBuilder();

            using (var contentEntryStream = entry.Open())
            {


                var settings = new XmlWriterSettings
                {
                    // Необходимый параметр для формирования вложенности тегов
                    ConformanceLevel = ConformanceLevel.Auto
                };


                using (var writer = XmlWriter.Create(sb, settings))
                {
                    contentEntryStream.Position = 0;
                    var reader = XmlReader.Create(contentEntryStream);
                    while (reader.Read())
                    {
                        switch(reader.NodeType)
                        {
                            case XmlNodeType.ProcessingInstruction:
                            case XmlNodeType.XmlDeclaration:
                                writer.WriteProcessingInstruction("xml", reader.Value);
                                break;

                            case XmlNodeType.Text:
                                var chunks = magazine.Where(v => reader.Value.Contains(v.Key));
                                var resultString = reader.Value;
                                foreach (var chunk in chunks)
                                {
                                    resultString = resultString.Replace(chunk.Key, chunk.Value);
                                    magazine.Remove(chunk.Key);
                                }
                                writer.WriteString(resultString);
                                break;

                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                                for (var i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    writer.WriteAttributeString(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.Value);
                                }
                                reader.MoveToElement();
                                if (reader.IsEmptyElement)
                                    writer.WriteEndElement();
                                break;

                            case XmlNodeType.EndElement:
                                writer.WriteEndElement();
                                break;
                        }
                    }

                }
            }

            entry.Delete();
            entry = archive.CreateEntry("content.xml");

            using (var writer = new StreamWriter(entry.Open()))
            {
                writer.Write(sb);
            }
        }

        public static Stream ConvertToPDF(Stream inputStream, string extension)
        {
            throw new NotImplementedException();
        }
    }
}
