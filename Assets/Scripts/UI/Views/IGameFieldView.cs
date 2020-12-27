public interface IGameFieldView
{
    void GenerateField(int difficulty, int width, int height);
    void SetState(int x, int y, bool state);
}
