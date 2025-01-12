using Game.GamePlay.Root.View;
using Game.GameRoot;
using System;
using UnityEngine;

namespace Game.GamePlay.Root
{
  public class GameplayEntryPoint : MonoBehaviour
  {
    public event Action GoToMainMenuSceneRequested;

    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
      UIGamePlayRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
      uiRoot.AttachSceneUI(uiScene.gameObject);

      uiScene.GoToMainMenuButtonClicked += () =>
      {
        GoToMainMenuSceneRequested?.Invoke();
      };
    }
  }
}
