using System;
using UnityEngine;

namespace Game.GamePlay.Root.View
{
  public class UIGamePlayRootBinder : MonoBehaviour
  {
    public event Action GoToMainMenuButtonClicked;

    public void HandleGoToMainMenuButtonClick() =>
      GoToMainMenuButtonClicked?.Invoke();
  }
}