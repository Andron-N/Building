using Game.GamePlay.Root;
using Game.MainMenu.Root;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game.GameRoot
{
  public class GameEntryPoint
  {
    private const string UI_ROOT_PATH = "UIRoot";
    private static GameEntryPoint _instance;
    private readonly Coroutines _coroutines;
    private readonly UIRootView _uiRoot;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
      _instance = new GameEntryPoint();
      _instance.StartGame();
    }

    private GameEntryPoint()
    {
      _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
      Object.DontDestroyOnLoad(_coroutines.gameObject);

      UIRootView prefabUIRoot = Resources.Load<UIRootView>(UI_ROOT_PATH);
      _uiRoot = Object.Instantiate(prefabUIRoot);
      Object.DontDestroyOnLoad(_uiRoot.gameObject);
    }

    private void StartGame()
    {
#if UNITY_EDITOR
      string sceneName = SceneManager.GetActiveScene().name;

      if (sceneName == Scenes.GAMEPLAY)
      {
        GameplayEnterParams enterParams = new GameplayEnterParams("save.save", 1);
        _coroutines.StartCoroutine(LoadAndStartGamePlay(enterParams));
        return;
      }

      if (sceneName == Scenes.MAIN_MENU)
      {
        _coroutines.StartCoroutine(LoadAndStartMainMenu());
        return;
      }

      if (sceneName != Scenes.BOOT)
        return;
#endif

      _coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartGamePlay(GameplayEnterParams enterParams)
    {
      _uiRoot.ShowLoadingScreen();

      yield return LoadScene(Scenes.BOOT);
      yield return LoadScene(Scenes.GAMEPLAY);

      yield return new WaitForSeconds(1);

      GameplayEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();

      sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(gameplayExitParams =>
      {
        _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
      });

      _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
    {
      _uiRoot.ShowLoadingScreen();

      yield return LoadScene(Scenes.BOOT);
      yield return LoadScene(Scenes.MAIN_MENU);

      yield return new WaitForSeconds(1);

      MainMenuEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

      sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(mainMenuExitParams =>
      {
        string targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

        if (targetSceneName == Scenes.GAMEPLAY)
          _coroutines.StartCoroutine(LoadAndStartGamePlay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
      });

      _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
      yield return SceneManager.LoadSceneAsync(sceneName);
    }
  }
}
