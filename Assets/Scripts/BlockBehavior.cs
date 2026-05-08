using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BlockBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public static event Action OnPerfectPlacement;
    public static event Action<float> OnImperfectPlacement;
    public static event Action BlockPlaced;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BlockBehavior>() != null) 
        {
            Vector3 normal = collision.contacts[0].normal;
            float dotUP = Vector3.Dot(normal, Vector3.up);
            if (dotUP > 0.9f)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.freezeRotation = true;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.useGravity = false;
                CalculateOffCenter(collision);
                transform.SetParent(collision.transform.parent);
                BlockPlaced?.Invoke();
            }
        } 
    }

    private void CalculateOffCenter(Collision collision)
    {
        float distance = Mathf.Abs(transform.position.x - collision.transform.position.x);
        if (distance < 0.1f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = collision.transform.position.x;
            transform.position = newPosition;
            OnPerfectPlacement?.Invoke();
        }
        else
        {
            OnImperfectPlacement?.Invoke(distance);
        }
    }
}
