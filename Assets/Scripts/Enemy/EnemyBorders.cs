using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorders : MonoBehaviour
{
    [SerializeField] private float _Down_Step;

    [SerializeField] private float _Distance_To_Last_Line;
    
    private BorderShips _Border_Ships;

    public event Action _On_Border_Collided; 

    private Vector3 _Screen;

    private Transform _Transform;
    
    private void Awake()
    {
        EnemyFactory._On_Squad_Changed += ChangeBorders;
        
        _Screen = ScreenSize.Borders();

        _Transform = transform;
    }
    
    
    public void CheckBorders()
    {
        if (_Border_Ships._Right_Border_Ship.transform.position.x >= _Screen.x)
        {
            _Transform.position = new Vector3(_Transform.position.x, _Transform.position.y - _Down_Step);
            _Border_Ships._Right_Border_Ship.transform.position = new Vector3(_Screen.x, _Border_Ships._Right_Border_Ship.transform.position.y);

            _On_Border_Collided?.Invoke();
        }
            
        else if (_Border_Ships._Left_Border_Ship.transform.position.x <= -_Screen.x)
        {
            _Transform.position = new Vector3(_Transform.position.x, _Transform.position.y - _Down_Step);
            _Border_Ships._Left_Border_Ship.transform.position = new Vector3(-_Screen.x, _Border_Ships._Left_Border_Ship.transform.position.y);

            _On_Border_Collided?.Invoke();
        }
        
        else if (_Border_Ships._Down_Border_Ship.transform.position.y <= _Screen.y - _Distance_To_Last_Line)
            GameEvents.OnPlayerLost();

    }
    
    private void ChangeBorders(List<EnemyShip> _squad)
    {
        if (_squad.Count == 0)
            return;

        EnemyShip _left_Ship = _squad[0];
        EnemyShip _right_Ship = _squad[0];
        EnemyShip _down_Ship = _squad[0];

        EnemyShip _current_Ship;
        
        for (int i = 0; i < _squad.Count; i++)
        {
            _current_Ship = _squad[i];

            _left_Ship = _current_Ship.transform.position.x < _left_Ship.transform.position.x ? _current_Ship : _left_Ship;
            _right_Ship = _current_Ship.transform.position.x > _right_Ship.transform.position.x ? _current_Ship : _right_Ship;
            _down_Ship = _current_Ship.transform.position.y < _down_Ship.transform.position.y ? _current_Ship : _down_Ship;

        }

        _Border_Ships = new BorderShips()
        {
            _Left_Border_Ship = _left_Ship,
            _Right_Border_Ship = _right_Ship,
            _Down_Border_Ship = _down_Ship
        };

    }
    
    private void OnDestroy() => EnemyFactory._On_Squad_Changed -= ChangeBorders;
}