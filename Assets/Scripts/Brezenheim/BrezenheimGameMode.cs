using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    private ILineGenerator _lineGenerator;
    private Line[] _lines;
    private List<Position>[] _LinePoints;
    private List<int>[] _Ds;
    private Position _last_point;
    private Position _prev_point;
    private int _iteration;
    private int _cur_line;
    private int _linesQuantity;
    private GameField _gameField;
    private InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(difficulty)
    {
        textField = nextTextField;
        _gameField = inputField;
        
        _Ds = new List<int>[_linesQuantity];
        _LinePoints = new List<Position>[_linesQuantity];
        _lines = new Line[_linesQuantity];

        GenerateLines();

        textField.text = _Ds[0][0].ToString();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void CheckAction(Pixel invoker)
    {
        if (!CanCheckAction()) return;

        if (_prev_point == _last_point)
        {
            _cur_line++;
            if (_cur_line == _linesQuantity)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
                eventReactor.OnGameOver();
                return;
            }
            else
            {
                _iteration = 0;
                _gameField.ClearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                _gameField.grid[(int)_lines[_cur_line].GetStart().X, (int)_lines[_cur_line].GetStart().Y].setPixelState(true);
                _last_point = _LinePoints[_cur_line][_LinePoints[_cur_line].Count - 1];
                _gameField.grid[(int)_last_point.X, (int)_last_point.Y].setPixelState(true);
              
                _prev_point = null;
                textField.text = _Ds[_cur_line][_iteration].ToString();
            }
        }
        else
        {
            _prev_point = _LinePoints[_cur_line][_iteration];

            if (invoker.X == _prev_point.X && invoker.Y == _prev_point.Y)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.setPixelState(true);
                _iteration++;
                textField.text = _Ds[_cur_line][_iteration].ToString();
                _prev_point = _LinePoints[_cur_line][_iteration];
            }
            else
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.ClearGrid();
        for (var i = 0; i < _linesQuantity; i++)
        {
            _Ds[i].Clear();
        }
        _cur_line = 0;
        _iteration = 0;
        GenerateLines();
        

        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    private void GenerateLines()
    {
        int minLength;
        int maxLength;
        int maxLengthSum;
        switch (difficulty)
        {
            case 1:
                _linesQuantity = 7;
                minLength = 4;
                maxLength = 8;
                maxLengthSum = 48;
                break;
            case 2:
                _linesQuantity = 10;
                minLength = 5;
                maxLength = 10;
                maxLengthSum = 90;
                break;
            default:
                _linesQuantity = 5;
                minLength = 2;
                maxLength = 5;
                maxLengthSum = 20;
                break;
        }
        _lineGenerator = new RandomLineGenerator(minLength, maxLength, maxLengthSum);
        var lines = _lineGenerator.Generate(_linesQuantity);
        for (var i = 0; i < _linesQuantity; i++)
            _lines[i] = lines[i];
    }

    ~BrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }
}
