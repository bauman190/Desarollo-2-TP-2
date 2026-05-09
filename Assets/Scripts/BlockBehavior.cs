using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BlockBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public event Action OnPerfectPlacement;
    public event Action<float> OnImperfectPlacement;
    public  event Action <GameObject> BlockCollided;

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
                rb.isKinematic = true;
                rb.useGravity = false;
                CalculateOffCenter(collision);
                transform.SetParent(collision.transform.parent);
                BlockCollided?.Invoke(collision.gameObject);
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
