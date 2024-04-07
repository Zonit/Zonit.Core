using System.Xml.Serialization;
using System.Xml;

namespace Zonit.Core.Xml;

public class XmlConvertible
{
    protected XmlConvertible() { }

    protected XmlConvertible(string xml)
    {
        if (string.IsNullOrEmpty(xml))
            throw new ArgumentException("XML string cannot be null or empty.", nameof(xml));

        var serializer = new XmlSerializer(this.GetType());

        using var reader = new StringReader(xml);
        var response = serializer.Deserialize(reader);

        if (response is not null)
        {
            var properties = response.GetType().GetProperties();
            foreach (var prop in properties)
                this.GetType().GetProperty(prop.Name)?.SetValue(this, prop.GetValue(response));
        }
        else
            throw new InvalidOperationException("Failed to deserialize XML into model.");
    }

    /// <summary>
    /// Converting the model to XML
    /// </summary>
    /// <returns></returns>
    public string Serialize()
    {
        using var stringWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });

        var serializer = new XmlSerializer(this.GetType());
        serializer.Serialize(xmlWriter, this);

        return stringWriter.ToString();
    }
}