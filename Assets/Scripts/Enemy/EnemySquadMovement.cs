using System;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(EnemyBorders))]
public class EnemySquadMovement : MonoBehaviour
{
    [SerializeField] private float _Start_Speed;
    
    [SerializeField] private float _Speed_Step;

    [SerializeField] private Vector3 _Start_Position;
    
    private CompositeDisposable _Disposable = new CompositeDisposable();

    private float _Current_Speed;
    
    private EnemyBorders _Borders;
    
    private Vector3 _Direction;

    private Transform _Transform;
    
    public enum Direction
    {
        Left,
        Right
    }

    public Direction _Start_Direction;

    private void Awake()
    {
        _Borders = GetComponent<EnemyBorders>();

        _Borders._On_Border_Collided += ChangeDirection;
        
        GameEvents._On_Player_Lost += StopMove;
        
        GameEvents._On_Wave_Started += Init;
        
        _Transform = transform;
    }
    
    public void Init()
    {
        StartDirection();
        Move();
    }

    private void StartDirection()
    {
        if (_Start_Direction == Direction.Left)
            _Direction = Vector3.left;
        else
            _Direction = Vector3.right;
    }

    private void ChangeDirection()
    {
        _Direction = -_Direction;
        _Current_Speed += _Speed_Step;
    }
    
    private void Move()
    {
        _Disposable.Clear();
        
        _Transform.position = _Start_Position;
        
        _Current_Speed = _Start_Speed;

        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                _Borders.CheckBorders();
                
                _Transform.Translate(_Direction * _Current_Speed * Time.deltaTime);

            }).AddTo(_Disposable);
    }

    private void StopMove() => _Disposable.Clear();

    private void OnDestroy()
    {
        _Disposable.Clear();
        
        _Borders._On_Border_Collided -= ChangeDirection;
        
        GameEvents._On_Wave_Started -= Init;
        
        GameEvents._On_Player_Lost -= StopMove;
    }
}