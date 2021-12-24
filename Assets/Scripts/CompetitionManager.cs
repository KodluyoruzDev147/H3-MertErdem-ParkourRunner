using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.UI;

public class CompetitionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static List<GameObject> competitors;
    //external classes
    [SerializeField] private CanvasController canvasController;

    private void Awake()
    {
        competitors = new List<GameObject>();
    }

    private void LateUpdate()
    {
        int playerRank = GetPlayerRank();
        canvasController.SetPlayerRankText(playerRank + 1);
    }

    private int GetPlayerRank()
    {
        var rankTable = competitors.OrderByDescending(x => x.transform.position.z).ToList();
        int playerRank = rankTable.IndexOf(player);

        return playerRank;
    }

    public static void JoinCompetition(GameObject competitor) => competitors.Add(competitor);

    public static void LeaveCompetition(GameObject competitor) => competitors.Remove(competitor);
}
