public class BrezenheimGameMode : GameMode
{
    public BrezenheimGameMode(GameplayTimer timer, int difficulty) : base(timer, difficulty)
    {

    }

    public override void CheckAction(Pixel invoker) => throw new System.NotImplementedException();

    public override void Restart() => throw new System.NotImplementedException();
}
