using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Joystick _Joystick;
    
    [SerializeField] private Button _Shoot_Button;
    [SerializeField] private Button _Change_Ammunition_Button;
    [SerializeField] private Button _Restart_Button;
    [SerializeField] private Button _Pause_Button;
    [SerializeField] private Button _Continue_Button;

    [SerializeField] private TextMeshProUGUI _Score;
    [SerializeField] private TextMeshProUGUI _Ammunition_Count;

    [SerializeField] private GameObject _Lose_Panel;
    [SerializeField] private GameObject _Pause_Panel;

    public Joystick GetJoystick() => _Joystick;
    public Button GetShootButton() => _Shoot_Button;
    public Button GetAmmunitionButton() => _Change_Ammunition_Button;
    public Button GetRestartButton() => _Restart_Button;
    public Button GetPauseButton() => _Pause_Button;
    public Button GetContinueButton() => _Continue_Button;

    public void ShowScore(int _score) => _Score.text = _score.ToString("0");
    
    public void CurrentAmmunitionCount(int _count) => _Ammunition_Count.text = _count.ToString("0");
    public void ShowAmmunitionCount() => _Ammunition_Count.gameObject.SetActive(true);
    public void HideAmmunitionCount() => _Ammunition_Count.gameObject.SetActive(false);
    public void ChangeAmmunitionTextColor(Color _color) => _Ammunition_Count.color= _color;

    public void ShowLosePanel() => _Lose_Panel.SetActive(true);
    
    public void ShowPausePanel() => _Pause_Panel.SetActive(true);
    
    public void HidePausePanel() => _Pause_Panel.SetActive(false);
}