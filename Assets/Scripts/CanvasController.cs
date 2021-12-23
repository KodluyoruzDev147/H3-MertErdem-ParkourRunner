using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject canvasMenu, canvasGame, canvasEnd;

    private void Awake()
    {
        GameManager.ActionStart += SetInGameUI;
        GameManager.ActionFinish += SetEndGameUI;
    }

    private void SetInGameUI()
    {
        canvasMenu.SetActive(false);
        canvasGame.SetActive(true);
    }

    private void SetEndGameUI()
    {
        canvasGame.SetActive(false);
        canvasEnd.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.ActionStart -= SetInGameUI;
        GameManager.ActionFinish -= SetEndGameUI;
    }
}
