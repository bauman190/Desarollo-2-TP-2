using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    //private float upDistance = 1f;
    private void Start()
    {
        TowerBehavior.BlockPlaced += RaiseCamera;
    }

    private void OnDestroy()
    {
        TowerBehavior.BlockPlaced -= RaiseCamera;
    }

    private void RaiseCamera()
    {
        float blockHeight = blockPrefab.GetComponent<Renderer>().bounds.size.y;

        transform.position += Vector3.up * 1f;
    }
}
