namespace Pixelgrid
{
    public interface IModeDataBuilder
    {
        void ResetObject();
        void SetName(string modeName);
        void SetDifficulty(int difficulty);
    }
}
