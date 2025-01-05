using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private string poolerName;
    [SerializeField] private GameObject[] objectsToCreate;
    [SerializeField] private int objectsPerItem;

    private List<GameObject> createdInstances = new List<GameObject>();
    private GameObject poolerContainer;

    private void Awake()
    {
        poolerContainer = new GameObject($"Pooler - {poolerName}");
        CreatePooler();
    }

    private void CreatePooler()
    {
        for (int i = 0; i < objectsToCreate.Length; i++)
        {
            for (int j = 0; j < objectsPerItem; j++)
            {
                createdInstances.Add(AddInstance(objectsToCreate[i]));
            }
        }
    }

    private GameObject AddInstance(GameObject obj)
    {
        GameObject newObj = Instantiate(obj, poolerContainer.transform);
        newObj.name = obj.name;
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetInstanceFromPooler(string name)
    {
        for (int i = 0; i < createdInstances.Count; i++)
        {
            if (createdInstances[i].name == name)
            {
                if (createdInstances[i].activeSelf == false)
                {
                    return createdInstances[i];
                }
            }
        }

        return null;
    }
}

