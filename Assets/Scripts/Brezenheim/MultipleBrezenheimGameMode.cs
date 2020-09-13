using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : BrezenheimGameMode
{
    public MultipleBrezenheimGameMode(GameplayTimer timer, int diff, GameField inputField, InputField nextTextField) : base(timer, diff, inputField, nextTextField)
    {

    }

    protected override void GenerateLines()
    {
        int linesCount;
        int minLength;
        int maxLength;
        switch (difficulty)
        {
            case 1:
                linesCount = 7;
                minLength = 4;
                maxLength = 8;
                break;
            case 2:
                linesCount = 10;
                minLength = 5;
                maxLength = 10;
                break;
            default:
                linesCount = 5;
                minLength = 2;
                maxLength = 5;
                break;
        }

        lines = new List<Line>(linesCount);
        lineGenerator = new PolygonLineGenerator(minLength, maxLength);
        var lns = lineGenerator.Generate(linesCount);
        linePoints = new List<Position>[linesCount];
        ds = new List<int>[linesCount];
        for (var i = 0; i < linesCount; i++)
        {
            linePoints[i] = new List<Position>();
            ds[i] = new List<int>();
        }

        for (var i = 0; i < linesCount; i++)
        {
            lines[i] = lns[i];
            linePoints[i] = Algorithms.GetBrezenheimLineData(lines[i], out var dds);
            ds[i] = dds;
        }

        lastPoint = linePoints[0][linePoints[0].Count - 1];
        gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].setPixelState(true);
        gameField.grid[(int)lines[0].GetEnd().X, (int)lines[0].GetEnd().Y].setPixelState(true);
    }
}
