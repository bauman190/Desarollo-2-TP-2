using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class TowerWobble : MonoBehaviour
{
    
    [SerializeField] private float sensitivity = 2f; 
    [SerializeField] private float returnForce = 1f;

    [SerializeField] private float limitAngle = 10f;
    public event Action OnTowerCollapsed;

    private Rigidbody rb;
    private float currentWobbleIntensity;
    private TowerBehavior towerMainLogic;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        towerMainLogic = GetComponentInParent<TowerBehavior>();
        rb.angularDamping = 2f;
        rb.centerOfMass = new Vector3(0, -1f, 0);
    }

    private void Start()
    {
        towerMainLogic.OnImperfectPlacementDetected += AddWobble;
        towerMainLogic.OnPerfectPlacementDetected += ReduceWobble;
    }

    private void FixedUpdate()
    {
        float angleZ = transform.eulerAngles.z;
        if (angleZ > 180)
        {
            angleZ -= 360;
        }

        float inclinacionActualMundo = Vector3.Angle(Vector3.up, transform.up);
        
        if (inclinacionActualMundo >= limitAngle)
        {
            OnTowerCollapsed?.Invoke();
            return;
        }

        
        rb.AddTorque(Vector3.forward * -angleZ * returnForce, ForceMode.Acceleration);

        
        if (currentWobbleIntensity > 0.01f)
        {
            float sideForce = Mathf.Sin(Time.time * 3f) * currentWobbleIntensity;
            rb.AddTorque(Vector3.forward * sideForce, ForceMode.Acceleration);
        }
    }

    private void OnDestroy()
    {
        towerMainLogic.OnImperfectPlacementDetected -= AddWobble;
        towerMainLogic.OnPerfectPlacementDetected -= ReduceWobble;
    }
   

    private void AddWobble(float distance)
    {
        currentWobbleIntensity += (distance * sensitivity);
    }

    private void ReduceWobble()
    {
        currentWobbleIntensity *= 0.2f; 
        rb.angularVelocity *= 0.5f;
    }
}