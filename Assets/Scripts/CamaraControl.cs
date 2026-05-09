using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private void Start()
    {
        BlockBehavior.BlockPlaced += RaiseCamera;
    }

    private void OnDestroy()
    {
        BlockBehavior.BlockPlaced -= RaiseCamera;
    }

    private void RaiseCamera()
    {
        float blockHeight = blockPrefab.GetComponent<Renderer>().bounds.size.y;

        transform.position += Vector3.up * blockHeight;
    }
}
