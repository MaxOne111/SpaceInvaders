using System;

public class GameEvents
{
    public static event Action _On_Wave_Started;
    public static event Action _On_Wave_Finished;
    public static event Action<int> _On_Enemy_Killed;
    public static event Action _On_Player_Lost;
    
    public static void OnWaveFinished() => _On_Wave_Finished?.Invoke();
    public static void OnWaveStarted() => _On_Wave_Started?.Invoke();

    public static void OnEnemyKilled(int _score) => _On_Enemy_Killed?.Invoke(_score);

    public static void OnPlayerLost() => _On_Player_Lost?.Invoke();
}