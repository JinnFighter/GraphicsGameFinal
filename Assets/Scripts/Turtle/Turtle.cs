using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public Pixel originalPixel;
    private IDirectionState _directionState;

    public Position Position { get; set; }
    private readonly int angle = 90;
    public readonly float offsetX;
    public readonly float offsetY;

    public void SetDirectionState(IDirectionState directionState) => _directionState = directionState;

    void Awake()
    {
        _directionState = new UpDirectionState();
    }

    public void RotateLeft()
    {
        this.gameObject.transform.Rotate(0, 0, angle);
        _directionState.RotateLeft(this);
    }

    public void RotateRight()
    {
        this.gameObject.transform.Rotate(0, 0, -angle);
        _directionState.RotateRight(this);
    }

    public void MoveForward() => _directionState.Move(this);
}