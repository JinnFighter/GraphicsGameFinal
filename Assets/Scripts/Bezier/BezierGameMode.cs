public class BezierGameMode : GameMode
{
    private LinesModeData _linesData;
    private ILineDataGenerator _generator;
    private ILinePointsGenerator _pointsGenerator;
    private GameField _gameField;

    public BezierGameMode(GameplayTimer timer, int difficulty, GameField field) : base(difficulty)
    {
        _gameField = field;
        _linesData = new LinesModeData();
        _pointsGenerator = new BezierLinePointsGenerator();
        Generate();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void CheckAction(Pixel invoker)
    {
        if (!CanCheckAction()) return;

        if (_linesData.IsCurrentLast())
            return;
        else
        {
            if (invoker.X == _linesData.GetCurrentPoint().X && invoker.Y == _linesData.GetCurrentPoint().Y)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                _linesData.NextPoint();
                if (_linesData.IsCurrentLast())
                    eventReactor.OnGameOver();
            }
            else
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
    }

    public override void DoRestartAction()
    {
        _gameField.ClearGrid();

        Generate();
    }

    private void Generate()
    {
        int pointsCount;
        int minLength;
        int maxLength;
        switch (difficulty)
        {
            case 1:
                pointsCount = 5;
                minLength = 4;
                maxLength = 6;
                break;
            case 2:
                pointsCount = 7;
                minLength = 5;
                maxLength = 7;
                break;
            default:
                pointsCount = 3;
                minLength = 3;
                maxLength = 5;
                break;
        }

        _generator = new BezierLineDataGenerator(pointsCount);
        _linesData = _generator.GenerateData(minLength, maxLength);

        var points = _pointsGenerator.Generate(_linesData);
        _gameField.Draw(points);
    }
}
