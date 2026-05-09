using UnityEngine;

public class BlockVisualHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] visualPrefabs;

    private void Start()
    {
        SpawnRandomVisual();
    }

    private void SpawnRandomVisual()
    {

        int randomIndex = Random.Range(0, visualPrefabs.Length);

        GameObject selectedPrefab = Instantiate(visualPrefabs[randomIndex],transform);

        selectedPrefab.transform.localPosition = new Vector3(0f, -0.5f, -0.5f);
        selectedPrefab.transform.localRotation = Quaternion.identity;
    }
}
