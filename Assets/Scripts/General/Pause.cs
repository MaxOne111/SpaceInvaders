using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button _Restart_Button;
    
    private GameUI _Game_UI;

    private GameObject _Pause_Panel;
    
    
    private Button _Pause_Button;
    private Button _Continue_Button;

    [Inject]
    private void Construct(GameUI _game_UI)
    {
        _Game_UI = _game_UI;
    }
    
    private void Awake()
    {
        _Pause_Button = _Game_UI.GetPauseButton();
        _Continue_Button = _Game_UI.GetContinueButton();
        
        _Restart_Button.onClick.AddListener(Restart);
        _Pause_Button.onClick.AddListener(StopGame);
        _Continue_Button.onClick.AddListener(ContinueGame);
    }
    
    private void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StopGame()
    {
        _Game_UI.ShowPausePanel();
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        _Game_UI.HidePausePanel();
    }
}