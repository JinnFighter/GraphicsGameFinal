using System.Collections.Generic;

public class FillGameFieldAction : IGameFieldAction
{
    private readonly IEnumerable<Position> _positions;

    public FillGameFieldAction(IEnumerable<Position> positions)
    {
        _positions = positions;
    }

    public void DoAction(GameFieldController gameField)
    {
        foreach (var position in _positions)
            gameField.SetState((int)position.X, (int)position.Y, true);
    }
}
