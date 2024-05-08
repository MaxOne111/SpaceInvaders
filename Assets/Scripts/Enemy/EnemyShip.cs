using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : MonoBehaviour, IEnemy
{
   [SerializeField] private Projectile _Projectile_Prefab;

   [SerializeField] private Transform _Muzzle;

   [SerializeField] private ParticleSystem _Explosion;

   [SerializeField] private int _Points_For_Kill;
   
   [SerializeField] private ProjectileSelectable _Droppable_Ammunition;
   [Range(0,1)]
   [SerializeField] private float _Ammunition_Drop_Chance;
   
   private event Action<EnemyShip> _On_Killed;

   private Transform _Projectile_Pool;

   public void Init(Action<EnemyShip> _action) => _On_Killed += _action;
   
   public void Shoot(Transform _pool)
   {
      _Projectile_Pool = _pool;
      
      Projectile _projectile = ObjectPool.PoolInstantiate(_Projectile_Prefab, _Muzzle, _Projectile_Pool);

      if (_projectile)
         _projectile.Init(ReturnToPool);
   }

   public void Death()
   {
      DropAmmunition();
      
      Instantiate(_Explosion, transform.position, Quaternion.identity);
      
      GameEvents.OnEnemyKilled(_Points_For_Kill);
      
      _On_Killed?.Invoke(this);
   }

   private void DropAmmunition()
   {
      if (!_Droppable_Ammunition)
         return;
      
      float _chance = Random.Range(0, 1f);

      if (_chance > _Ammunition_Drop_Chance)
         return;

      Instantiate(_Droppable_Ammunition, transform.position, _Droppable_Ammunition.transform.rotation);
   }

   private void ReturnToPool(Transform _projectile) => ObjectPool.ReturnToPool(_projectile, _Muzzle, _Projectile_Pool);

   private void OnDisable() => _On_Killed = null;

   private void OnDestroy() => _On_Killed = null;
}