using System.Xml;
using System.Xml.Serialization;

public class PlayerProfile
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
}
