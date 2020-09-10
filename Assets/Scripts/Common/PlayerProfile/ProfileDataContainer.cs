using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract(Name = "ProfileDataContainer")]
[KnownType(typeof(List<ProfileData>))]
public class ProfileDataContainer
{
    [DataMember(Name = "Datas")]
    public List<ObjectData> datas;

    public ProfileDataContainer(List<PlayerProfile> profiles)
    {
        datas = new List<ObjectData>();
        foreach(var profile in profiles)
        {
            datas.Add(profile.ExportToData());
        }
    }

    public ProfileDataContainer()
    {
        datas = new List<ObjectData>();
    }
}
