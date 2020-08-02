using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

public class XmlParser
{
    public object ImportProfiles(string data, Type type)
    {
        var serializer = new DataContractSerializer(type);
        var stringReader = new StringReader(data);
        var xmlReader = new XmlTextReader(stringReader);
        return serializer.ReadObject(xmlReader);
    }

    public string ExportProfiles(object data)
    {
        var serializer = new DataContractSerializer(data.GetType());
        var stringWriter = new StringWriter();
        var xmlWriter = new XmlTextWriter(stringWriter);
        serializer.WriteObject(xmlWriter, data);
        return stringWriter.ToString();
    }
}
