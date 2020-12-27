public class CellData
{
    private Position _position;
    private bool _state;

    public CellData(Position position, bool state)
    {
        _position = position;
        _state = state;
    }

    public Position GetPosition() => _position;

    public bool IsActive() => _state;

    public void SetState(bool state) => _state = state;
}
