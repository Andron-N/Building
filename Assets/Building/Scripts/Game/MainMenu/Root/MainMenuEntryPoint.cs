using Game.GameRoot;
using Game.MainMenu.Root.View;
using System;
using UnityEngine;

namespace Game.MainMenu.Root
{
  public class MainMenuEntryPoint : MonoBehaviour
  {
    public event Action GoToGameplaySceneRequested;

    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
      UIMainMenuRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
      uiRoot.AttachSceneUI(uiScene.gameObject);

      uiScene.GoToGameplayButtonClicked += () =>
      {
        GoToGameplaySceneRequested?.Invoke();
      };
    }
  }
}
