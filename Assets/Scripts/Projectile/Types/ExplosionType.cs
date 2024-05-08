using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Explosion", fileName = "ExplosionType")]
public class ExplosionType : ProjectileType
{
    [SerializeField] private float _Radius;
    
    public override void Effect(Transform _transform_Point)
    {
        Collider2D[] _targets = Physics2D.OverlapCircleAll(_transform_Point.position, _Radius);
        
        if (_targets.Length == 0)
            return;
        
        for (int i = 0; i < _targets.Length; i++)
            if (_targets[i].TryGetComponent(out EnemyShip _ship))
                _ship.Death();
    }
    
}