using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BlockType
{
    Standard,
    Wagons,
    Trains
}

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType blockType;
    [SerializeField] private bool hasSlope;

    [SerializeField] private Train[] trains;
    [SerializeField] private GameObject[] diamonds;

    public BlockType BlockType => blockType;
    public bool HasSlope => hasSlope;

    private List<GameObject> diamondList = new List<GameObject>();
    private bool areDiamondsReferenced;
    private Train selectedTrain;

    public void InitializeBlock()
    {
        if (blockType == BlockType.Trains)
        {
            SelectTrain();
        }

        GetDiamonds();
        ActivateDiamonds();
    }

    private void GetDiamonds()
    {
        if (areDiamondsReferenced)
        {
            return;
        }

        foreach (GameObject parent in diamonds)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject diamond = parent.transform.GetChild(i).gameObject;
                diamondList.Add(diamond);
            }
        }

        areDiamondsReferenced = true;
    }

    private void ActivateDiamonds()
    {
        if (diamondList.Count == 0 || diamondList == null)
        {
            return;
        }

        foreach (GameObject diamond in diamondList)
        {
            diamond.SetActive(true);
        }
    }

    private void SelectTrain()
    {
        if (trains == null || trains.Length == 0)
        {
            return;
        }

        int index = Random.Range(0, trains.Length);
        trains[index].gameObject.SetActive(true);
        selectedTrain = trains[index];

        ResetTrainPosition(selectedTrain);
    }


    private void ResetTrainPosition(Train train)
    {

        float[] xPositions = { -2.32f, -5.28f, 0.76f }; 

        int trainIndex = System.Array.IndexOf(trains, train);

        if (trainIndex != -1)
        {

            train.transform.position = new Vector3(xPositions[trainIndex], 0.1f, 56.84f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (selectedTrain != null)
            {
                selectedTrain.CanMove = true;
                selectedTrain.Player = other.GetComponent<PlayerController>();
            }
        }
    }
}
