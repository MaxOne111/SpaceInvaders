using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyShip[] _Prefabs;
    
    [SerializeField] private Transform _Parent;
    [SerializeField] private Transform _Enemy_Pool;
    [Header("Squad size")]
    [SerializeField] private int _Rows;
    [SerializeField] private int _Columns;
    
    [SerializeField] private float _Step;

    public static event Action<List<EnemyShip>> _On_Squad_Changed;

    [SerializeField] private List<EnemyShip> _Enemy_Squad = new List<EnemyShip>();

    private void Awake() => GameEvents._On_Wave_Started += Init;

    public void Init() => CreateSquad();

    public EnemyShip Create(Vector3 _position, Quaternion _rotation)
    {
        int _index;
        if (_Prefabs.Length == 0)
        {
            Debug.LogError("Array is empty");
            return null;
        }

        if (_Prefabs.Length == 1)
            _index = 0;
        else
            _index = Random.Range(0, _Prefabs.Length);
        
        EnemyShip _ship = ObjectPool.PoolInstantiate(_Prefabs[_index], _position, _rotation, _Enemy_Pool);
        
        _ship.Init(ReturnToPool);
        _ship.transform.SetParent(_Parent);
        
        return _ship;
    }
    
    private void CreateSquad()
    {
        if (_Rows == 0 || _Columns == 0)
            return;

        float _half = _Columns / 2f;
        float _offset = 0.5f;

        for (int i = 0; i < _Rows; i++)
        for (int j = 0; j < _Columns; j++)
        {
            EnemyShip _ship = Create(new Vector3((_Parent.transform.position.x -_half + j + _offset) * _Step,_Parent.position.y - i,0), Quaternion.identity);
            
            _Enemy_Squad.Add(_ship);
        }
        
        _On_Squad_Changed?.Invoke(_Enemy_Squad);
    }
    
    private void ReturnToPool(EnemyShip _ship)
    {
        _Enemy_Squad.Remove(_ship);
        
        _On_Squad_Changed?.Invoke(_Enemy_Squad);

        ObjectPool.ReturnToPool(_ship, _Parent, _Enemy_Pool);
        
        if (_Enemy_Squad.Count == 0)
            GameEvents.OnWaveStarted();
    }

    private void OnDestroy() => GameEvents._On_Wave_Started -= Init;
}