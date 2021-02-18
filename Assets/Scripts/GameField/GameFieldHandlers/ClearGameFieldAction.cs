public class ClearGameFieldAction : IGameFieldAction
{
    public void DoAction(GameFieldController gameField) => gameField.ClearGrid();
}
