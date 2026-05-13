using UnityEngine;

public class TowerWobble : MonoBehaviour
{
    [SerializeField] private float maxOffset = 2f;
    [SerializeField] private float maxWobble = 15f;
    [SerializeField] private float wobbleSpeed = 2f;
    [SerializeField] private float wobbleReduction = 10f;

    private float wobbleAmount;

    private TowerBehavior towerMainLogic;

    private void Awake()
    {
        towerMainLogic = GetComponentInParent<TowerBehavior>();
    }

    private void Start()
    {
        towerMainLogic.OnImperfectPlacementDetected += AddWobble;
        towerMainLogic.OnPerfectPlacementDetected += ReduceWobble;
    }

    private void OnDestroy()
    {
        towerMainLogic.OnImperfectPlacementDetected -= AddWobble;
        towerMainLogic.OnPerfectPlacementDetected -= ReduceWobble;
    }
    private void Update()
    {
        float angle = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void AddWobble(float distance)
    {
        float t = Mathf.Clamp01(distance / maxOffset);
        wobbleAmount += t * maxWobble;
    }

    private void ReduceWobble()
    {
        wobbleAmount -= wobbleReduction;
        wobbleAmount = Mathf.Max(wobbleAmount, 0f);
    }
}