using UnityEngine;

public class GameBorders : MonoBehaviour
{
    [SerializeField] private Transform _Border_Up;
    [SerializeField] private Transform _Border_Down;
    
    [SerializeField] private float _Offset_Up;
    [SerializeField] private float _Offset_Down;

    private void Start() => InstallBorders();

    private void InstallBorders()
    {
        Vector3 _screen = ScreenSize.Borders();

        _Border_Up.position = new Vector3(0, _screen.y + _Offset_Up);
        _Border_Down.position = new Vector3(0, -_screen.y - _Offset_Down);
    }
}