using Game.GameRoot;

namespace Game.MainMenu.Root
{
  public class MainMenuExitParams
  {
    public SceneEnterParams TargetSceneEnterParams { get; }

    public MainMenuExitParams(SceneEnterParams targetSceneEnterParams) =>
      TargetSceneEnterParams = targetSceneEnterParams;
  }
}
