using UnityEngine;
using Zenject;

public class ScoreCounter : MonoBehaviour
{
    private PlayerScore _Player_Score = new PlayerScore();
    
    private  GameUI _Game_UI;

    [Inject]
    private void Construct(GameUI _game_UI)
    {
        _Game_UI = _game_UI;
    }
    private void Awake() => GameEvents._On_Enemy_Killed += AddScore;

    private void Start()
    {
        _Player_Score.Reset();
        _Game_UI.ShowScore(PlayerScore._Score);
    }

    private void AddScore(int _value)
    {
        _Player_Score.AddScore(_value);
        
        _Game_UI.ShowScore(PlayerScore._Score);
    }

    private void OnDestroy() => GameEvents._On_Enemy_Killed -= AddScore;

}