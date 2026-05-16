using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private PlayerContaroler player;

    public event Action<float> OnImperfectPlacementDetected;
    public event Action OnPerfectPlacementDetected;

    private List<BlockBehavior> blocksInTower = new List<BlockBehavior>();

    public static event Action BlockPlaced;

    private BlockBehavior nextBlock = null;

    private float maxPoints = 100;
    private float minPoints = 10;
    private float score = 0;
    private float maxScore;
    private int streak = 0;

    public event Action GameOver;

    public event Action<float, float, int, int> UpdateScore;
    private bool gameOver = false;

    private TowerWobble towerWobble;

    private void Awake()
    {
        towerWobble = GetComponentInChildren<TowerWobble>();
    }
    private void Start()
    { 
        player.BlockGenerated += GetNextBlock;
        towerWobble.OnTowerCollapsed += HandleTowerColaps;
        maxScore = PlayerPrefs.GetFloat("MaxScore", 0);
        UpdateScore?.Invoke(score, maxScore, streak, blocksInTower.Count);
    }

    private void Update()
    {

        UpdateMaxScore();

        if (!gameOver && MissedLastBlock())
        {
            UpdateMaxScore();
            GameOver?.Invoke();
        }
    }

    private void OnDestroy()
    {
        player.BlockGenerated -= GetNextBlock;
    }

    private void CalculateWobble(float distance)
    {
        if (blocksInTower.Count > 0)
        {
            OnImperfectPlacementDetected.Invoke(distance);
            streak = 0;
            IncreaseScore(distance);
        }
    }

    private void ApplayWobbleReduction()
    {
        if (blocksInTower.Count > 0)
        {
            OnPerfectPlacementDetected.Invoke();
            streak++;
            IncreaseScore(0);
        }
    }

    private void AddBlock(BlockBehavior objetoChocado)
    {
        if (CollitionWithLastBlock(objetoChocado) && blocksInTower.Count > 0)
        {
            UpdateMaxScore();
            GameOver?.Invoke();
        }
        else
        {
            blocksInTower.Add(nextBlock);
            nextBlock.OnImperfectPlacement -= CalculateWobble;
            nextBlock.OnPerfectPlacement -= ApplayWobbleReduction;
            nextBlock.BlockCollided -= AddBlock;
            UpdateScore?.Invoke(score, maxScore, streak, blocksInTower.Count);
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

    private void UpdateMaxScore()
    {
        if(score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetFloat("MaxScore", maxScore);
        }
    }
   
    private void IncreaseScore(float distance)
    {
        if (distance > 0)
        {
            float maxDistance = 1f;
            float normalized = Mathf.Clamp01(distance / maxDistance);

            score += Mathf.RoundToInt(Mathf.Lerp(maxPoints, minPoints, normalized));
        }
        else
        {
            score += maxPoints * 2;
        }
    }

    private void HandleTowerColaps()
    {
        GameOver?.Invoke();
    }
}
