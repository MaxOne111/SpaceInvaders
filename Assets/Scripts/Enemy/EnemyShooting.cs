using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private float _Max_Fire_Rate;
    [SerializeField] private float _Min_Fire_Rate;
    [SerializeField] private float _Start_Delay;

    [SerializeField] private Transform _Projectile_Pool;
    
    private CompositeDisposable _Disposable = new CompositeDisposable();
    
    private List<EnemyShip> _Enemy_Squad = new List<EnemyShip>();

    private float _Last_Fire;
    
    private void Awake()
    {
        EnemyFactory._On_Squad_Changed += CurrentEnemySquad;
        
        GameEvents._On_Player_Lost += StopShoot;
    }

    private void Start() => Shoot();

    private void CurrentEnemySquad(List<EnemyShip> _squad) => _Enemy_Squad = _squad;

    private void Shoot()
    {
        Observable.EveryUpdate()
            .Delay(TimeSpan.FromSeconds(_Start_Delay))
            .Where(_=> Time.timeScale != 0)
            .Where(_=> _Enemy_Squad.Count > 0)
            .Subscribe(_ =>
            {
                if (Time.time < _Last_Fire)
                        return;

                _Last_Fire = Time.time + 1f / Random.Range(_Min_Fire_Rate, _Max_Fire_Rate);
                
                int _index = Random.Range(0, _Enemy_Squad.Count);
                
                _Enemy_Squad[_index].Shoot(_Projectile_Pool);

            }).AddTo(_Disposable);
    }

    private void StopShoot() => _Disposable.Clear();

    private void OnDestroy()
    {
        EnemyFactory._On_Squad_Changed -= CurrentEnemySquad;
        
        _Disposable.Clear();
        
        GameEvents._On_Player_Lost -= StopShoot;
    }
}