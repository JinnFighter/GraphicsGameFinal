using System.Collections.Generic;

public abstract class ObjectData
{
    public Dictionary<string, object> Data;

    public ObjectData()
    {
        Data = new Dictionary<string, object>();
    }

    public T ReadObject<T>(string key) => (T)Data[key];

    public void WriteObject(string key, object value) => Data[key] = value;
}
