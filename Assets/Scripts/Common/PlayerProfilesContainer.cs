using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("PlayersCollection")]
public class PlayerProfilesContainer
{
    [XmlArray("Players")]
    [XmlArrayItem("Player")]
    public List<PlayerProfile> profiles = new List<PlayerProfile>();

    public static PlayerProfilesContainer Load(string path)
    {   
        _ = Resources.Load(path) as TextAsset;

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerProfilesContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PlayerProfilesContainer;
        }
    }

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(PlayerProfilesContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
}

