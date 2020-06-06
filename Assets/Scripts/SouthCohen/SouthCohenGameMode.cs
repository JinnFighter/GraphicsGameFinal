using System.Collections.Generic;

public class SouthCohenGameMode
{
    private Pixel[] borderPoints;
    private int linesQuantity;
    private Pixel[,] lines;
    private int iteration;
    private List<int>[] lineZones;
    //[SerializeField] private SpriteRenderer border;
    private int[,] gridCodes;
    private int gridCodesWidth;
    private int gridCodesHeight;
    private int maxLineLength;
    private int minLineLength;
}
