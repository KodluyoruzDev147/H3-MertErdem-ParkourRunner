using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject canvasMenu, canvasGame;

    private void Awake()
    {
        GameManager.ActionStart += SetInGameUI;
    }

    private void SetInGameUI()
    {
        canvasMenu.SetActive(false);
        canvasGame.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.ActionStart -= SetInGameUI;
    }
}
