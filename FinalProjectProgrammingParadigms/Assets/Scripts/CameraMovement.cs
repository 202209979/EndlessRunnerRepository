using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float characterHeight;
    [SerializeField] private float characterDistance;

    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 characterPosition = characterTransform.position;
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, characterPosition.x, Time.deltaTime * 10f);
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, characterPosition.y + characterHeight, Time.deltaTime * 10f);
        cameraPosition.z = Mathf.Lerp(cameraPosition.z, characterPosition.z + characterDistance, Time.deltaTime * 10f);
        transform.position = cameraPosition;
    }
}

