using Game.GamePlay.Root;
using Game.GameRoot;
using Game.MainMenu.Root.View;
using R3;
using UnityEngine;

namespace Game.MainMenu.Root
{
  public class MainMenuEntryPoint : MonoBehaviour
  {
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public Observable<MainMenuExitParams> Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
    {
      UIMainMenuRootBinder uiScene = Instantiate(_sceneUIRootPrefab);
      uiRoot.AttachSceneUI(uiScene.gameObject);

      Subject<Unit> exitSignalSubject = new Subject<Unit>();
      uiScene.Bind(exitSignalSubject);
      
      Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");

      string saveFileName = "save.save";
      int levelNumber = Random.Range(0, 100);
      
      GameplayEnterParams gamePlayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
      MainMenuExitParams mainMenuExitParams = new MainMenuExitParams(gamePlayEnterParams);
      Observable<MainMenuExitParams> exitToGamePlaySceneSignal = exitSignalSubject.Select(_ => mainMenuExitParams);
      
      return exitToGamePlaySceneSignal;
    }
  }
}
