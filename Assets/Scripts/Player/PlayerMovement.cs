using System;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float _Move_Speed;

   private CompositeDisposable _Disposable = new CompositeDisposable();
   
   private Joystick _Joystick;

   private GameUI _Game_UI;

   private Transform _Transform;

   private Vector3 _Move_Direction;

   private float _Move_X;
   private float _Move_Y;

   [Inject]
   private void Construct(GameUI _game_UI)
   {
      _Game_UI = _game_UI;
   }
   
   private void Awake()
   {
      _Joystick = _Game_UI.GetJoystick();
      _Transform = transform;

      GameEvents._On_Player_Lost += StopMove;
   }

   private void Start() => Movement();

   private void MoveDirection()
   {
      _Move_X = _Joystick.Horizontal;
      _Move_Y = _Joystick.Vertical;

      _Move_Direction = new Vector3(_Move_X, _Move_Y, 0);
   }
   
   private void Movement()
   {
      Observable.EveryUpdate().Subscribe(_ =>
      {
         MoveDirection();

         _Transform.Translate(_Move_Direction * _Move_Speed * Time.deltaTime);
      }).AddTo(_Disposable);
   }

   private void StopMove() => _Disposable.Clear();

   private void OnDestroy()
   {
      _Disposable.Clear();
      
      GameEvents._On_Player_Lost -= StopMove;
   }
}