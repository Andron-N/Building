using Game.GamePlay.Root.View;
using Game.GameRoot;
using Game.MainMenu.Root;
using R3;
using UnityEngine;

namespace Game.GamePlay.Root
{
  public class GameplayEntryPoint : MonoBehaviour
  {
    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    public Observable<GameplayExitParams> Run(UIRootView uiRoot, GameplayEnterParams enterParams)
    {
      UIGamePlayRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
      uiRoot.AttachSceneUI(uiScene.gameObject);

      Subject<Unit> exitSceneSignalSubject = new Subject<Unit>();
      uiScene.Bind(exitSceneSignalSubject);

      Debug.Log($"GAMEPLAY ENTRY POINT: save file name = {enterParams.SaveFileName}, level to load = {enterParams.LevelNumber}");

      MainMenuEnterParams mainMenuEnterParams = new MainMenuEnterParams("Fatality");
      GameplayExitParams exitParams = new GameplayExitParams(mainMenuEnterParams);
      Observable<GameplayExitParams> exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => exitParams);

      return exitToMainMenuSceneSignal;
    }
  }
}
