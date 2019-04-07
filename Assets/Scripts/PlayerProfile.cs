using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class PlayerProfile
{

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Active")]
    public bool active;
}
