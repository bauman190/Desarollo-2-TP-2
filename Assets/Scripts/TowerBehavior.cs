using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private PlayerContaroler player;

    [SerializeField] private float maxOffset = 2f;
    [SerializeField] private float maxWobble = 15f;
    [SerializeField] private float wobbleSpeed = 2f;
    [SerializeField] private float wobbleReduction = 10f;
    private float wobbleAmount;
    private List<BlockBehavior> blocksInTower = new List<BlockBehavior>();

    public static event Action BlockPlaced;

    private BlockBehavior nextBlock = null;

    private void Start()
    { 
        player.BlockGenerated += GetNextBlock;
    }

    private void Update()
    {
        float angle = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnDestroy()
    {
        
    }

    private void CalculateWobble(float distance)
    {
        if (blocksInTower.Count > 0)
        {
            float t = Mathf.Clamp01(distance / maxOffset);

            wobbleAmount += t * maxWobble;
        }
    }

    private void ApplayWobbleReduction()
    {
        if (blocksInTower.Count > 0)
        { 
            wobbleAmount -= wobbleReduction;

            wobbleAmount = Mathf.Max(wobbleAmount, 0f);
        }
    }

    private void AddBlock(GameObject objetoChocado)
    {
        if(blocksInTower.Count > 0)
        {

        }
        blocksInTower.Add(nextBlock);
        nextBlock.OnImperfectPlacement -= CalculateWobble;
        nextBlock.OnPerfectPlacement -= ApplayWobbleReduction;
        nextBlock.BlockCollided -= AddBlock;
        BlockPlaced?.Invoke();
    }

    private BlockBehavior GetLastBlock()
    {
        if (blocksInTower.Count > 0)
        {
            return blocksInTower[blocksInTower.Count - 1];
        }
        return null;
    }

    private void GetNextBlock(BlockBehavior block)
    {
        nextBlock = block;
        nextBlock.OnImperfectPlacement += CalculateWobble;
        nextBlock.OnPerfectPlacement += ApplayWobbleReduction;
        nextBlock.BlockCollided += AddBlock;
    }

}
