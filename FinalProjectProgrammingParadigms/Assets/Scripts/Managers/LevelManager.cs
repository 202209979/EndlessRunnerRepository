using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int startingBlocks = 5;
    [SerializeField] private int maxBlocksForWagons = 5;
    [SerializeField] private int maxBlocksForTrains = 10;
    [SerializeField] private int maxTrainBlocksForReset = 2;
    [SerializeField] private Block mainBlock;
    [SerializeField] private float standardBlockLength = 38.65f;
    [SerializeField] private float trainBlockLength = 77f;
    [SerializeField] private float postTrainBlockExtraLength = 3.69f;
    [SerializeField] private Block[] prefabBlocks;

    private List<Block> standardBlocks = new List<Block>();
    private List<Block> wagonBlocks = new List<Block>();
    private List<Block> trainBlocks = new List<Block>();
    private List<Block> slopeBlocks = new List<Block>();

    private Pooler pooler;
    private Block lastBlock;
    private int generatedBlocks;

    private void Awake()
    {
        pooler = GetComponent<Pooler>();
    }

    void Start()
    {
        AddBlocksByType();
        lastBlock = mainBlock;

        for (int i = 0; i < startingBlocks; i++)
        {
            CreateBlock();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CreateBlock();
        }
    }

    private void CreateBlock()
    {
        if (generatedBlocks >= maxBlocksForTrains)
        {
            if (generatedBlocks < maxBlocksForTrains + 1)
            {
                AddBlock(BlockType.Trains, standardBlockLength);
            }
            else
            {
                AddBlock(BlockType.Trains, trainBlockLength);
            }

            if (generatedBlocks == maxBlocksForTrains + maxTrainBlocksForReset)
            {
                generatedBlocks = 0;
            }
        }
        else if (generatedBlocks >= maxBlocksForWagons)
        {
            AddBlock(BlockType.Wagons, standardBlockLength);
        }
        else
        {
            if (generatedBlocks == maxBlocksForWagons - 1)
            {
                AddBlock(BlockType.Standard, standardBlockLength, true);
            }
            else
            {
                if (lastBlock.BlockType == BlockType.Trains)
                {
                    AddBlock(BlockType.Standard, trainBlockLength + postTrainBlockExtraLength);
                }
                else
                {
                    AddBlock(BlockType.Standard, standardBlockLength);
                }
            }
        }
    }

    private void AddBlock(BlockType type, float lenght, bool hasSlope = false)
    {
        Block newBlock = GetBlockByType(type, hasSlope);
        Vector3 newPos = NewBlockPosition(lenght);
        newBlock.transform.position = newPos;
        lastBlock = newBlock;
        generatedBlocks++;
    }

    private Block GetBlockByType(BlockType type, bool hasSlope = false)
    {
        Block newBlock = null;
        if (hasSlope)
        {
            newBlock = GetInstanceFromPooler(slopeBlocks);
        }

        else
        {
            switch (type)
            {
                case BlockType.Standard:
                    newBlock = GetInstanceFromPooler(standardBlocks);
                    break;
                case BlockType.Wagons:
                    newBlock = GetInstanceFromPooler(wagonBlocks);
                    break;
                case BlockType.Trains:
                    newBlock = GetInstanceFromPooler(trainBlocks);
                    break;
            }
        }

        if (newBlock != null)
        {
            newBlock.InitializeBlock();
        }

        return newBlock;
    }

    private Vector3 NewBlockPosition(float length)
    {
        Vector3 newPosition = lastBlock.transform.position + Vector3.forward * length;
        Debug.Log($"New Position: {newPosition}, Length: {length}, BlockType: {lastBlock.BlockType}");
        return newPosition;
    }
    private Block GetInstanceFromPooler(List<Block> list)
    {
        int randomBlock = Random.Range(0, list.Count);
        string blockName = list[randomBlock].name;
        GameObject instance = pooler.GetInstanceFromPooler(blockName);
        instance.transform.position = Vector3.zero; // Resetea posición
        instance.transform.rotation = Quaternion.identity; // Resetea rotación
        instance.SetActive(true);
        Block block = instance.GetComponent<Block>();
        return block;
    }


    private void AddBlocksByType()
    {
        foreach (Block block in prefabBlocks)
        {
            switch (block.BlockType)
            {
                case BlockType.Standard:
                    standardBlocks.Add(block);
                    if (block.HasSlope)
                    {
                        slopeBlocks.Add(block);
                    }
                    break;
                case BlockType.Wagons:
                    wagonBlocks.Add(block);
                    break;
                case BlockType.Trains:
                    trainBlocks.Add(block);
                    break;

            }
        }
    }


    private void AnswerRequest()
    {
        CreateBlock();
    }

    private void OnEnable()
    {
        Regenerator.NewBlockRequestEvent += AnswerRequest;
    }

    private void OnDisable()
    {
        Regenerator.NewBlockRequestEvent -= AnswerRequest;

    }





}
