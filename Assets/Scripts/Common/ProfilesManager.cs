using UnityEngine;

public class ProfilesManager : MonoBehaviour
{
    public static string path = "Data/players_base.xml";

    public PlayerProfile ActiveProfile { get; set; }
    public PlayerProfilesContainer Container { get; set; }

    // Start is called before the first frame update
    void Start()
    {
       Container = PlayerProfilesContainer.Load(path);

        foreach (var profile in Container.profiles)
        {
            if(profile.active)
                ActiveProfile = profile;
        }
    }
}
