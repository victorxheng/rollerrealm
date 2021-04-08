using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeModeEndless : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -10.0f;
    private float tileLength = 40.0f;
    private float safeZone = 80.0f;
    private int amountOnScreen = 4;

    private int Index;

    private List<GameObject> activeTiles;
    private List<GameObject> activeArrows;

    // Use this for initialization
    void Start()
    {
        Index = PlayerPrefs.GetInt("CubefrequencyIndex", 0);
        activeTiles = new List<GameObject>();
        activeArrows = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amountOnScreen; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amountOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[Index]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);        
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    public void changeFrequency()
    {
        Index = PlayerPrefs.GetInt("CubefrequencyIndex", 0);
    }
}
