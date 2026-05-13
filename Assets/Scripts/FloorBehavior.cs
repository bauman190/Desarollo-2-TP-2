using UnityEngine;

public class FloorBehavior : MonoBehaviour
{
    [SerializeField] private Transform blockContainer;


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.SetParent(blockContainer);
    }
}
