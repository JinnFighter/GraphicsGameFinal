using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilesManager : MonoBehaviour
{
    public const string path = "Assets/Data/players_base.xml";
    private PlayerProfile activeProfile;
    public PlayerProfile ActiveProfile { get => activeProfile; set => activeProfile = value; }
    private PlayerProfilesContainer container;
    public PlayerProfilesContainer Container { get => container; set => container = value; }
    

    // Start is called before the first frame update
    void Start()
    {
       Container = PlayerProfilesContainer.Load(path);

        foreach (PlayerProfile profile in container.profiles)
        {
            if(profile.active)
            {
                activeProfile = profile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
