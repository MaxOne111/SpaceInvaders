using System;
using UniRx;
using UnityEngine;

public class PlayerBorders : MonoBehaviour
{
    [SerializeField] private float _Y_Limit;
    
    private Vector3 _Borders;

    private CompositeDisposable _Disposable = new CompositeDisposable();
    
    private Transform _Transform;

    private void Awake()
    {
        _Transform = transform;
        _Borders = ScreenSize.Borders();
    }

    private void Start() => CheckBorders();

    private void CheckBorders()
    {
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                if (_Transform.position.x >= _Borders.x)
                    _Transform.position = new Vector3(_Borders.x, _Transform.position.y);
                else if(_Transform.position.x <= -_Borders.x)
                    _Transform.position = new Vector3(-_Borders.x, _Transform.position.y);
                
                if (_Transform.position.y >= _Borders.y - _Y_Limit)
                    _Transform.position = new Vector3(_Transform.position.x, _Borders.y - _Y_Limit);
                else if(_Transform.position.y <= -_Borders.y)
                    _Transform.position = new Vector3(_Transform.position.x, -_Borders.y);
                    
            }).AddTo(_Disposable);
    }

    private void OnDestroy() => _Disposable.Clear();
}