using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : Singleton<DiamondManager>
{
    public int TotalDiamonds { get; private set; }
    private string diamondsKey = "myDiamonds";

    protected override void Awake()
    {
        base.Awake();
        TotalDiamonds = PlayerPrefs.GetInt(diamondsKey);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.X))
       {
            AddDiamonds(1);

       }
    }
    public void AddDiamonds(int quantity)
    {
        TotalDiamonds += quantity;
        PlayerPrefs.SetInt(diamondsKey, TotalDiamonds);
        PlayerPrefs.Save();
    }

    public void SpendDiamonds(int quantity)
    {
        if (TotalDiamonds >= quantity)
        {
            TotalDiamonds -= quantity;
            PlayerPrefs.SetInt(diamondsKey, TotalDiamonds);
            PlayerPrefs.Save();
        }
    }

}
