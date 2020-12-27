public class NullEventReactor : IEventReactor
{
    public bool CanCheckAction() => false;

    public void OnChangeState(int difficulty)
    {
        
    }

    public void OnContinue()
    {
       
    }

    public void OnGameOver()
    {

    }

    public void OnPause()
    {

    }

    public void OnRestart()
    {

    }
}
