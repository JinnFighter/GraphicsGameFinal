using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    [SerializeField] private GameFieldView _view;
    private NewGameField _gameField;
    public int Difficulty { get; private set; }

    void Awake()
    {
        Difficulty = PlayerPrefs.GetInt("difficulty");
        _gameField = new NewGameField(Difficulty);
        _view.GenerateField(Difficulty, _gameField.GetWidth(), _gameField.GetHeight());
    }

    public void SetState(int x, int y, bool state)
    {
        _gameField.GetData(x, y).SetState(state);
        _view.SetState(x, y, state);
    }

    public void ClearGrid()
    {
        for (var i = 0; i < _gameField.GetWidth(); i++)
            for (var j = 0; j < _gameField.GetHeight(); j++)
            {
                _gameField.GetData(i, j).SetState(false);
                _view.SetState(i, j, false);
            }
    }
}
