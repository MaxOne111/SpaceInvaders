using System;
using UnityEngine;

public class PlayerReactions : MonoBehaviour
{
   private PlayerShooting _Player_Shooting;

   private void Awake() => _Player_Shooting = GetComponent<PlayerShooting>();

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.TryGetComponent(out ProjectileSelectable _projectile))
         _Player_Shooting.AddAmmunition(_projectile.Ammunition);

   }
}