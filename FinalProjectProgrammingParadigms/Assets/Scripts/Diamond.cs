using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private int diamondValue = 1;
    
    private void ObtainDiamond()
    {
        DiamondManager.Instance.AddDiamonds(diamondValue);
        GameManager.Instance.ObtainedDiamonds += diamondValue;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObtainDiamond();
        }
    }
}
