using System;
using UnityEngine;

public class ProjectileSelectable : MonoBehaviour
{
    [SerializeField] private Ammunition _Ammunition;

    public Ammunition Ammunition => _Ammunition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IEnemy _enemy))
            return;
        
        if (col.TryGetComponent(out Projectile _projectile))
            return;
        
        Destroy(gameObject);
    }
    
}