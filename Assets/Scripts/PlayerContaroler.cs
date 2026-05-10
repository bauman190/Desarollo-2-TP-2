using UnityEngine;
using System;

public class PlayerContaroler : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private GameObject block = null;

    [SerializeField] private float speed = 10;
    [SerializeField] Transform floor;
    private Bounds area;

    public event Action <BlockBehavior> BlockGenerated;

    private void Awake()
    {
        area = floor.GetComponent<Renderer>().bounds;
    }

    private void Start()
    {
        GenerateBlock();
        TowerBehavior.BlockPlaced += RaisePlayer;
        TowerBehavior.BlockPlaced += GenerateBlock;
    }

    private void Update()
    {
        DropBlock();
        Movement();
    }

    private void OnDestroy()
    {
        TowerBehavior.BlockPlaced -= RaisePlayer;
        TowerBehavior.BlockPlaced -= GenerateBlock;
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
            BlockBehavior newBlock = block.GetComponent<BlockBehavior>();
            BlockGenerated?.Invoke(newBlock);
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

        transform.position += Vector3.up * 1f;
    }

}


