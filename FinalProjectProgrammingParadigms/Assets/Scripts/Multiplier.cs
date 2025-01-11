using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.MultiplierValue = value;
            GameManager.Instance.StartMultiplierCount(duration);
            gameObject.SetActive(false);
        }
    }
}
