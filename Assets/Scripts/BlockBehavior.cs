using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BlockBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public event Action OnPerfectPlacement;
    public event Action<float> OnImperfectPlacement;
    public  event Action <BlockBehavior> BlockCollided;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision)
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
                if (audioSource != null)
                {
                    audioSource.Play();
                    Destroy(audioSource, audioSource.clip.length);
                }
                BlockCollided?.Invoke(collision.gameObject.GetComponent<BlockBehavior>());
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
