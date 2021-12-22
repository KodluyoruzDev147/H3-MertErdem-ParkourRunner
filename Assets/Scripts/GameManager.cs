using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction ActionStart;

    //start button's method
    public void ActivateActionStart() => ActionStart?.Invoke();
}
