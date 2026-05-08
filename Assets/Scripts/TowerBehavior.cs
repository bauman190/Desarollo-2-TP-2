using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private float maxOffset = 2f;
    [SerializeField] private float maxWobble = 15f;
    [SerializeField] private float wobbleSpeed = 2f;
    [SerializeField] private float wobbleReduction = 10f;
    private float wobbleAmount;
    private int blocksCount = 0;

    void Start()
    {
        BlockBehavior.OnImperfectPlacement += CalculateWobble;
        BlockBehavior.OnPerfectPlacement += ApplayWobbleReduction;
        BlockBehavior.BlockPlaced += IncreaseBlockCount;
    }
    
    void Update()
    {
        float angle =
        Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    void OnDestroy()
    {
        BlockBehavior.OnImperfectPlacement -= CalculateWobble;
        BlockBehavior.OnPerfectPlacement -= ApplayWobbleReduction;
        BlockBehavior.BlockPlaced -= IncreaseBlockCount;
    }

    private void CalculateWobble(float distance)
    {
        if (blocksCount > 0)
        {
            float t = Mathf.Clamp01(distance / maxOffset);

            wobbleAmount += t * maxWobble;
        }
    }
    private void ApplayWobbleReduction()
    {
        if (blocksCount > 0)
        { 
            wobbleAmount -= wobbleReduction;

            wobbleAmount = Mathf.Max(wobbleAmount, 0f);
        }
    }
    private void IncreaseBlockCount()
    {
        blocksCount++;
    }
}
