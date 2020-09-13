using System.Collections.Generic;

public interface ILinePointsGenerator
{
    List<Position> Generate(LinesModeData linesData);
}
