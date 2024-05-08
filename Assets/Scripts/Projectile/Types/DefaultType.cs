using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Default", fileName = "DefaultType")]
public class DefaultType : ProjectileType
{
    public override void Effect(Transform _transform_Point)
    {
        if (_transform_Point.TryGetComponent(out EnemyShip _ship))
            _ship.Death();
    }
}