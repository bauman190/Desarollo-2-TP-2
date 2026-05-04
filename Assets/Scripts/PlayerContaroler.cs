using UnityEngine;

public class PlayerContaroler : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private GameObject block = null;

    private void Awake()
    {
        GenerateBlock();
    }

    private void Update()
    {
        GenerateBlock();
        DropBlock();
    }
    private void DropBlock()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            block.GetComponent<Collider>().enabled = true;
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
            block.GetComponent<Collider>().enabled = false;
        }
        
    }
}
