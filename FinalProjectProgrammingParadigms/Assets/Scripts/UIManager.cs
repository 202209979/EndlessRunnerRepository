using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Update()
    {
        diamondsText.text = GameManager.Instance.ObtainedDiamonds.ToString();
        scoreText.text = GameManager.Instance.Score.ToString();
        
    }
}
