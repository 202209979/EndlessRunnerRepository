using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalDiamondsText;
    [SerializeField] private TextMeshProUGUI bestScoreText;  

    private void Start()
    {
        int totalDiamonds = PlayerPrefs.GetInt("finalDiamondsKey", 0); 
        totalDiamondsText.text = totalDiamonds.ToString();      

        int bestScore = PlayerPrefs.GetInt("finalScoreKey", 0);       
        bestScoreText.text = bestScore.ToString();               
    }
}
