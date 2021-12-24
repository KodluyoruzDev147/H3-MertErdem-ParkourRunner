using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction ActionStart, ActionFinish;

    #region UI Buttons' Methods
    public void ActivateActionStart() => ActionStart?.Invoke();

    public void Restart() { }

    public void LoadNextLevel() { }
    #endregion
}
