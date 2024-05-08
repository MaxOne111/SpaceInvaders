using UnityEngine;

public static class ObjectPool
{
    private static Transform FreeObject(Transform _pool)
    {
        if (_pool.childCount == 0)
            return null;
        
        
        for (int i = 0; i < _pool.childCount; i++)
            if (_pool.GetChild(i))
                return _pool.GetChild(i);

        return null;
    }
    
    private static T FreeObject<T>(Transform _pool) where T : class
    {
        if (_pool.childCount == 0)
            return null;
        
        
        for (int i = 0; i < _pool.childCount; i++)
            if (_pool.GetChild(i))
                return _pool.GetChild(i).transform.GetComponent<T>();
            
                

        return null;
    }

    public static void ReturnToPool(Transform _object, Transform _spawn_Point, Transform _pool)
    {
        _object.SetParent(_pool);
        _object.position = _spawn_Point.position;
        _object.gameObject.SetActive(false);
    }
    
    public static void ReturnToPool<T>(T _object, Transform _spawn_Point, Transform _pool) where T: MonoBehaviour
    {
        _object.transform.SetParent(_pool);
        _object.transform.position = _spawn_Point.position;
        _object.gameObject.SetActive(false);
    }
    
    public static T PoolInstantiate<T>( T _prefab, Transform _spawn_Point,  Transform _pool) where T : MonoBehaviour
    {
        if (FreeObject(_pool))
        {
            FreeObject(_pool).gameObject.SetActive(true);
            FreeObject(_pool).position = _spawn_Point.position;
            FreeObject(_pool).SetParent(null);

            return null;
        }

        T _new_Oblect = Object.Instantiate(_prefab, _spawn_Point.position, _spawn_Point.rotation);
        return _new_Oblect;
    }
    
    public static T PoolInstantiate<T>( T _prefab, Vector3 _position, Quaternion _rotation,  Transform _pool) where T : MonoBehaviour
    {
        if (FreeObject<T>(_pool))
        {
            T _object = FreeObject<T>(_pool);
            
            _object.gameObject.SetActive(true);
            _object.transform.position = _position;
            _object.transform.SetParent(null);

            return _object;
        }

        T _new_Oblect = Object.Instantiate(_prefab, _position, _rotation);
        return _new_Oblect;
        
    }
}