using System;
using UniRx;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _Move_Speed;

    [Range(-1, 1)]
    [SerializeField] private int _Direction;
    
    [SerializeField] private ProjectileType _Type;
    
    public enum Marker
    {
        Player,
        Enemy
    }

    public Marker _Marker;
    
    private CompositeDisposable _Disposable = new CompositeDisposable();

    private Transform _Transform;

    private event Action<Transform> _On_Collided;

    private void Awake() => _Transform = transform;

    private void Start() => Move();

    public void Init(Action<Transform> _action) => _On_Collided += _action;

    private void Move()
    {
        Vector3 _direction = new Vector3(0, _Direction, 0);
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _Transform.Translate(_direction * _Move_Speed * Time.deltaTime);
         
        }).AddTo(_Disposable);
    }

    public void ChangeType(ProjectileType _type) => _Type = _type;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IEnemy _enemy))
        {
            if (_Marker == Marker.Enemy)
                return;
            
            _Type.Effect(col.transform);
        }
        
        if(col.TryGetComponent(out Projectile _projectile))
            return;
        
        if(col.TryGetComponent(out ProjectileSelectable _selectable))
            return;
        
        _On_Collided?.Invoke(_Transform);
    }

    private void OnDestroy()
    {
        _Disposable.Clear();
        _On_Collided = null;
    }
}