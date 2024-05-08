using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EnemyFactory _Factory;
    [SerializeField] private EnemySquadMovement _Squad;
    
    private void Start()
    {
        _Factory.Init();
        _Squad.Init();
    }
}