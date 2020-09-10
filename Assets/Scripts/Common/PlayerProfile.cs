using System.Xml;
using System.Xml.Serialization;

public class PlayerProfile : ISerialized
{
    [XmlAttribute("name")]
    public string name;

    [XmlElement("Active")]
    public bool active;

    public PlayerProfile()
    {

    }

    public PlayerProfile(string n, bool a)
    {
        name = n;
        active = a;
    }

    public ObjectData ExportToData()
    {
        var data = new ProfileData();
        data.WriteObject("Name", name);
        data.WriteObject("IsActive", active);
        return data;
    }

    public void ImportFromData(ObjectData data)
    {
        name = data.ReadObject<string>("Name");
        active = data.ReadObject<bool>("IsActive");
    }
}
