using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _Muzzle;
    
    [SerializeField] private Projectile _Projectile_Prefab;
    
    [SerializeField] private float _Fire_Speed;

    [SerializeField] private Transform _Projectile_Pool;
    
    [SerializeField] private ProjectileType _Default_Type;

    private CompositeDisposable _Disposable = new CompositeDisposable();

    private List<Ammunition> _Player_Ammunition = new List<Ammunition>();
    
    private Ammunition _Current_Ammunition;

    private int _Ammunition_Index = 0;

    private Button _Shoot_Button;
    private Button _Change_Ammunition_Button;

    private GameUI _Game_UI;
    
    [Inject]
    private void Construct(GameUI _game_UI)
    {
        _Game_UI = _game_UI;
    }

    private void Awake()
    {
        _Current_Ammunition = new Ammunition(_Default_Type, 0, true);
        
        _Player_Ammunition.Add(_Current_Ammunition);
        
        _Shoot_Button = _Game_UI.GetShootButton();
        _Change_Ammunition_Button = _Game_UI.GetAmmunitionButton();

        GameEvents._On_Player_Lost += StopShoot;
    }

    private void Start()
    {
        Shoot();
        ChangeAmmo();
    }

    private void Shoot()
    {
        _Shoot_Button.OnClickAsObservable()
            .Where(_=> _Current_Ammunition._Count > 0 || _Current_Ammunition._Is_Infinity)
            .ThrottleFirst(TimeSpan.FromSeconds(1f / _Fire_Speed))
            .Subscribe(_ =>
            {
                Projectile _projectile = ObjectPool.PoolInstantiate(_Projectile_Prefab, _Muzzle.position, _Muzzle.rotation, _Projectile_Pool);

                _projectile.Init(ReturnToPool);
                
                _projectile.ChangeType(_Current_Ammunition._Type);

                SpendAmmunition();
                
                _Game_UI.CurrentAmmunitionCount(_Current_Ammunition._Count);

            }).AddTo(_Disposable);
    }

    private void SpendAmmunition()
    {
        _Current_Ammunition.SpendAmmo();

        if (_Current_Ammunition._Count > 0 || _Current_Ammunition._Is_Infinity)
            return;
        
        _Player_Ammunition.Remove(_Current_Ammunition);
        
        NextAmmo();
    }

    private void ChangeAmmo()
    {
        _Change_Ammunition_Button.OnClickAsObservable()
            .Where(_=> _Player_Ammunition.Count > 0)
            .Subscribe(_ =>
            {
                NextAmmo();
                
            }).AddTo(_Disposable);
    }

    private void NextAmmo()
    {
        _Ammunition_Index++;

        if (_Ammunition_Index >= _Player_Ammunition.Count)
            _Ammunition_Index = 0;

        _Current_Ammunition = _Player_Ammunition[_Ammunition_Index];

        if (_Current_Ammunition._Is_Infinity)
        {
            _Game_UI.HideAmmunitionCount();
            return;
        }
                
        _Game_UI.ShowAmmunitionCount();
        _Game_UI.ChangeAmmunitionTextColor(_Current_Ammunition._Type.TextColor);
        _Game_UI.CurrentAmmunitionCount(_Current_Ammunition._Count);
    }

    public void AddAmmunition(Ammunition _ammunition)
    {
        foreach (Ammunition _type in _Player_Ammunition)
        {
            if (_type._Type == _ammunition._Type)
            {
                _type.AddAmmo(_ammunition._Count);
            
                _Game_UI.CurrentAmmunitionCount(_type._Count);
                
                return;
            }
        }
        
        _Player_Ammunition.Add(_ammunition);
    }
    
    private void StopShoot() => _Disposable.Clear();

    private void ReturnToPool(Transform _projectile) => ObjectPool.ReturnToPool(_projectile, _Muzzle, _Projectile_Pool);


    private void OnDestroy()
    {
        _Disposable.Clear();
        
        GameEvents._On_Player_Lost -= StopShoot;
    }
}