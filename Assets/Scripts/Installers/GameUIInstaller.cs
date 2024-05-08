using UnityEngine;
using Zenject;

public class GameUIInstaller : MonoInstaller
{
    [SerializeField] private GameUI _Game_UI;
    public override void InstallBindings() => Container.Bind<GameUI>().FromInstance(_Game_UI).AsSingle().NonLazy();
}