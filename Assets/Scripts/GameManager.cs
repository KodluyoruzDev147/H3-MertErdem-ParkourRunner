using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction ActionStart, ActionFinish;

    //start button's method
    public void ActivateActionStart() => ActionStart?.Invoke();
}
