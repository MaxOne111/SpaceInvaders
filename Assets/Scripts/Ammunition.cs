using System;
using UnityEngine;

[Serializable]
public class Ammunition
{
    public ProjectileType _Type;
    public int _Count;
    public bool _Is_Infinity;

    public Ammunition(ProjectileType _type, int _count, bool _isInfinity)
    {
        _Type = _type;
        _Count = _count;
        _Is_Infinity = _isInfinity;
    }
    
    public void AddAmmo(int _value)
    {
        if (_value < 0)
            return;

        _Count += _value;
    }

    public void SpendAmmo()
    {
        if (_Is_Infinity || _Count <= 0)
            return;
            
        _Count--;
    }
}