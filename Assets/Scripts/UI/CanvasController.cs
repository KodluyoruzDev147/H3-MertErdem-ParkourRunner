using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject canvasMenu, canvasGame, canvasEnd;
        [SerializeField] private TextMeshProUGUI textPlayerRank;

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

        public void SetPlayerRankText(int rank)
        {
            textPlayerRank.text = rank.ToString();
        }

        private void OnDestroy()
        {
            GameManager.ActionStart -= SetInGameUI;
            GameManager.ActionFinish -= SetEndGameUI;
        }
    }
}
