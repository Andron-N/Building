namespace Game.GameRoot
{
  public class SceneEnterParams
  {
    public string SceneName { get; }

    public SceneEnterParams(string sceneName) =>
      SceneName = sceneName;

    public T As<T>() where T : SceneEnterParams =>
      (T)this;
  }
}
