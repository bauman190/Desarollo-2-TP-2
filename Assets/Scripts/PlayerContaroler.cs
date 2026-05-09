using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerContaroler : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private GameObject block = null;

    [SerializeField] private float speed = 10;
    [SerializeField] Transform floor;
    private Bounds area;

    private void Awake()
    {
        area = floor.GetComponent<Renderer>().bounds;
    }

    private void Start()
    {
        GenerateBlock();
        BlockBehavior.BlockPlaced += RaisePlayer;
        BlockBehavior.BlockPlaced += GenerateBlock;
    }

    private void Update()
    {
        DropBlock();
        Movement();
    }

    private void OnDestroy()
    {
        BlockBehavior.BlockPlaced -= RaisePlayer;
        BlockBehavior.BlockPlaced -= GenerateBlock;
    }
    private void DropBlock()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            block.transform.SetParent(null);
            Rigidbody blockRigidbody = block.GetComponent<Rigidbody>();
            blockRigidbody.useGravity = true;
            block = null;
        }

    }
    private void GenerateBlock()
    {
        if (block == null)
        {
            block = Instantiate(blockPrefab, transform.position, Quaternion.identity, this.transform);
        }
        
    }

    private void Movement()
    {
        float x = transform.position.x + speed * Time.deltaTime;
        x = Mathf.Clamp(x, area.min.x, area.max.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        if (ReachesBorderLimit())
        {
            speed *= -1.0f;
        }
    }
    private bool ReachesBorderLimit()
    {
        return Mathf.Approximately(transform.position.x, area.min.x) || Mathf.Approximately(transform.position.x, area.max.x);
    }

    private void RaisePlayer()
    {
        float blockHeight = blockPrefab.GetComponent<Renderer>().bounds.size.y;

        transform.position += Vector3.up * blockHeight;
    }

}


