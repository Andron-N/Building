using R3;
using UnityEngine;

namespace Game.GamePlay.Root.View
{
  public class UIGamePlayRootBinder : MonoBehaviour
  {
    private Subject<Unit> _exitSceneSignalSubject;

    public void HandleGoToMainMenuButtonClick() =>
      _exitSceneSignalSubject?.OnNext(Unit.Default);

    public void Bind(Subject<Unit> exitSceneSignalSubject) =>
      _exitSceneSignalSubject = exitSceneSignalSubject;
  }
}