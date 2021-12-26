using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static UnityAction ActionStart, ActionFinish;

    #region UI Buttons' Methods
    public void ActivateActionStart() => ActionStart?.Invoke();

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        //int nextLevel = PlayerPrefs.GetInt("LEVEL", 0) + 1;
        //PlayerPrefs.SetInt("LEVEL", nextLevel);
        //SceneManager.LoadScene($"LEVEL{nextLevel}");
    }
    #endregion
}
