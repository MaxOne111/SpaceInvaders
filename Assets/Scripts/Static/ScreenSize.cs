using UnityEngine;

public static class ScreenSize
{
    private static float _Offset = 0.3f;
    
    public static Vector3 Borders()
    {
        Camera _camera = Camera.main;
        
        Vector3 _screen_Size = new Vector3(Screen.width, Screen.height);

        Vector3 _scene_Size = _camera.ScreenToWorldPoint(_screen_Size);

        Vector3 _borders = new Vector3(_scene_Size.x - _Offset, _scene_Size.y - _Offset);

        return _borders;
    }
}