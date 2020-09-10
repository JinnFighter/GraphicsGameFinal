public interface ISerialized
{
    ObjectData ExportToData();
    void ImportFromData(ObjectData data);
}
