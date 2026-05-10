using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

    public event Action GameOver;

    private bool gameOver = false;
    private void Start()
    { 
        player.BlockGenerated += GetNextBlock;
    }

    private void Update()
    {
        float angle = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (!gameOver && MissedLastBlock())
            GameOver?.Invoke();
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

    private void AddBlock(BlockBehavior objetoChocado)
    {
        if (CollitionWithLastBlock(objetoChocado) && blocksInTower.Count > 0)
        {
            GameOver?.Invoke();
        }
        else
        {
            blocksInTower.Add(nextBlock);
            nextBlock.OnImperfectPlacement -= CalculateWobble;
            nextBlock.OnPerfectPlacement -= ApplayWobbleReduction;
            nextBlock.BlockCollided -= AddBlock;
            BlockPlaced?.Invoke();
        }
    }

    private bool CollitionWithLastBlock(BlockBehavior objetoChocado)
    {
        if (blocksInTower.Count == 0)
            return false;

        return objetoChocado != blocksInTower[^1];
    }

    private bool MissedLastBlock()
    {
        if (blocksInTower.Count == 0)
            return false;

        if (nextBlock.transform.position.y < blocksInTower[^1].transform.position.y)
        {
            gameOver = true;
            return true;
        }

        return false;
    }

    private void GetNextBlock(BlockBehavior block)
    {
        nextBlock = block;
        nextBlock.OnImperfectPlacement += CalculateWobble;
        nextBlock.OnPerfectPlacement += ApplayWobbleReduction;
        nextBlock.BlockCollided += AddBlock;
    }

}
