using Game.GamePlay.Root;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game.GameRoot
{
  public class GameEntryPoint
  {
    private const string UIRootPath = "UIRoot";
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

      UIRootView prefabUIRoot = Resources.Load<UIRootView>(UIRootPath);
      _uiRoot = Object.Instantiate(prefabUIRoot);
      Object.DontDestroyOnLoad(_uiRoot.gameObject);
    }

    private void StartGame()
    {
#if UNITY_EDITOR
      string sceneName = SceneManager.GetActiveScene().name;

      if (sceneName == Scenes.GAMEPLAY)
      {
        _coroutines.StartCoroutine(LoadStartGamePlay());
        return;
      }

      if (sceneName != Scenes.BOOT)
        return;
#endif

      _coroutines.StartCoroutine(LoadStartGamePlay());
    }

    private IEnumerator LoadStartGamePlay()
    {
      _uiRoot.ShowLoadingScreen();

      yield return LoadScene(Scenes.BOOT);
      yield return LoadScene(Scenes.GAMEPLAY);

      yield return new WaitForSeconds(2);

      GameplayEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
      sceneEntryPoint.Run();

      _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
      yield return SceneManager.LoadSceneAsync(sceneName);
    }
  }
}
