using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

public class ProfilesManager : MonoBehaviour
{
    private string _dataPath = Path.Combine("Data");
    private string _path = Path.Combine("Data", "players_base.xml");

    public PlayerProfile ActiveProfile { get; set; }
    public PlayerProfilesContainer Container { get; set; }

    public void Load()
    {
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
        if (!File.Exists(_path))
        {
            using (XmlWriter writer = XmlWriter.Create(_path))
            {
                writer.WriteStartElement("PlayersCollection");
                writer.WriteStartElement("Players");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
            }; 
        }
        Container = PlayerProfilesContainer.Load(_path);

        if(Container.profiles.Any(profile => profile.active))
        {
            ActiveProfile = Container.profiles.First(profile => profile.active);
            ExcludeAllOtherActiveProfiles();
        }
    }

    public void Save()
    {
        ExcludeAllOtherActiveProfiles();
        Container.Save(_path);
    }

    private void ExcludeAllOtherActiveProfiles()
    {
        foreach (var profile in Container.profiles.Where(profile => profile.active && profile.name!= ActiveProfile.name))
            profile.active = false;
    }
}
