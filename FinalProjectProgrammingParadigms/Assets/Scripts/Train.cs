using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool CanMove { get;set; }
    public PlayerController Player { get;set; }
    void Update()
    {
        if (CanMove)
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
            if (transform.position.z + 40 < Player.transform.position.z)
            {
                CanMove = false;             
            }
        }
    }
}
