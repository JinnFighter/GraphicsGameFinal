using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
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
        TextAsset _xml_file = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerProfilesContainer));

        //StringReader reader = new StringReader(_xml_file.text);

        //PlayerProfilesContainer _profiles = serializer.Deserialize(reader) as PlayerProfilesContainer;
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PlayerProfilesContainer;
        }
        //reader.Close();

        //return _profiles;
    }
}
