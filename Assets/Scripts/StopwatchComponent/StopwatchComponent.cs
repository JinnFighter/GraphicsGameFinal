using UnityEngine;

public class StopwatchComponent : MonoBehaviour
{
    private float _currentTime;
    private bool _isCounting;

    public delegate void TimeTick(float currentTime);

    public event TimeTick TimeChange;

    void Awake()
    {
        ResetStopwatch();
        _isCounting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isCounting)
        {
            _currentTime += Time.deltaTime;
            TimeChange?.Invoke(_currentTime);
        }
    }

    public void Stop() => _isCounting = false;

    public void Resume() => _isCounting = true;

    public void ResetStopwatch() => _currentTime = 0.0000f;
}
