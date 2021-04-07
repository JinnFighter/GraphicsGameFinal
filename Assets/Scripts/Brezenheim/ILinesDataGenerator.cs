namespace Pixelgrid
{
    public interface ILinesDataGenerator
    {
        LineData GenerateData(int minLength, int maxLength);
    }
}
