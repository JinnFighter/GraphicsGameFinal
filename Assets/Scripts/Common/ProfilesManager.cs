using System.Linq;
using UnityEngine;

public class ProfilesManager : MonoBehaviour
{
    public static string path = "Data/players_base.xml";

    public PlayerProfile ActiveProfile { get; set; }
    public PlayerProfilesContainer Container { get; set; }

    public void Load()
    {
        Container = PlayerProfilesContainer.Load(path);

        if(Container.profiles.Any(profile => profile.active))
        {
            ActiveProfile = Container.profiles.First(profile => profile.active);
            ExcludeAllOtherActiveProfiles();
        }
    }

    public void Save()
    {
        ExcludeAllOtherActiveProfiles();
        Container.Save(path);
    }

    private void ExcludeAllOtherActiveProfiles()
    {
        foreach (var profile in Container.profiles.Where(profile => profile.active && profile.name!= ActiveProfile.name))
            profile.active = false;
    }
}
