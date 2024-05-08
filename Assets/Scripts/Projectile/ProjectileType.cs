using UnityEngine;

public abstract class ProjectileType : ScriptableObject
{
    [SerializeField] private Color _Text_Color;

    public Color TextColor => _Text_Color;
    public virtual void Effect(Transform _transform_Point){}
}