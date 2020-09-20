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

        var lines = new List<Line>(linesCount);
        lineGenerator = new PolygonLineGenerator(minLength, maxLength);
        var lns = lineGenerator.Generate(linesCount);
        linesDatas= new List<LinesModeData>(linesCount);
        ds = new List<int>[linesCount];
        for (var i = 0; i < linesCount; i++)
        {
            linesDatas[i] = new LinesModeData();
            ds[i] = new List<int>();
        }

        for (var i = 0; i < linesCount; i++)
        {
            lines[i] = lns[i];
            var linesdata = Algorithms.GetBrezenheimLineData(lines[i], out var dds);

            foreach (var ldata in linesdata)
                linesDatas[i].AddPoint(ldata);

            ds[i] = dds;
        }

        lastPoint = linesDatas[0].GetPoint(linesDatas[0].GetPointsCount() - 1);
        gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].SetState(true);
        gameField.grid[(int)lines[0].GetEnd().X, (int)lines[0].GetEnd().Y].SetState(true);
    }
}
