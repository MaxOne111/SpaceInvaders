using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PlayerDefeat : MonoBehaviour
{
   [SerializeField] private ParticleSystem _Explosion;
   
   private Button _Restart_Button;
   
   private GameUI _Game_UI;

   [Inject]
   private void Construct(GameUI _game_UI)
   {
      _Game_UI = _game_UI;
   }
   
   private void Awake()
   {
      GameEvents._On_Player_Lost += ShowLosePanel;
      
      _Restart_Button = _Game_UI.GetRestartButton();
      
      _Restart_Button.onClick.AddListener(Restart);
   }

   private void ShowLosePanel() => _Game_UI.ShowLosePanel();

   private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

   private void Death()
   {
      Instantiate(_Explosion, transform.position, Quaternion.identity);
      GameEvents.OnPlayerLost();
      gameObject.SetActive(false);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.TryGetComponent(out Projectile _projectile))
      {
         if (_projectile._Marker == Projectile.Marker.Player)
            return;
         
         Death();
      }
   }

   private void OnDestroy()
   {
      GameEvents._On_Player_Lost -= ShowLosePanel;
      
      _Restart_Button.onClick.RemoveListener(Restart);
   }
}
