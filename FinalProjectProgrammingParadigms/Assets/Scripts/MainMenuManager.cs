using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalDiamondsText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private void Start()
    {
        int totalDiamonds = DiamondManager.Instance.TotalDiamonds;
        totalDiamondsText.text = totalDiamonds.ToString();

        int bestScore = GameManager.Instance.BestScore;
        bestScoreText.text = bestScore.ToString();

    }
}
