using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndelessMap : MonoBehaviour {
    public GameObject[] tilePrefabs;
    public GameObject arrow;

    private Transform playerTransform;
    private float spawnZ = -150.0f;
    private float tileLength= 200.0f;
    private float safeZone = 400.0f;
    private int amountOnScreen = 3;
    private int TileNumber = 0;

    private int RandomIndex;

    private List<GameObject> activeTiles;
    private List<GameObject> activeArrows;

    // Use this for initialization
    void Start () {
        RandomIndex = 0;
        activeTiles = new List<GameObject>();
        activeArrows = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amountOnScreen; i++)
        {
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(playerTransform.position.z -safeZone> (spawnZ - amountOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
	}
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        GameObject middleArrow;
        go = Instantiate(tilePrefabs[RandomIndex]) as GameObject;
        middleArrow = Instantiate(arrow) as GameObject;
        go.transform.SetParent(transform);
        middleArrow.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        middleArrow.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
        activeArrows.Add(middleArrow);
        TileNumber++;

        if (TileNumber <= 1)
        {
            RandomIndex = Random.Range(1, 3);
        }
        else if(TileNumber <= 3)
        {
            RandomIndex = Random.Range(1, 4);
        }
        else if(TileNumber <= 5)
        {
            RandomIndex = Random.Range(2, 6);
        }
        else if(TileNumber <= 7)
        {
            RandomIndex = Random.Range(2, 8);
        }
        else if (TileNumber <= 9)
        {
            RandomIndex = Random.Range(3, 8);
        }
        else if (TileNumber <= 10)
        {
            RandomIndex = Random.Range(4, 10);
        }
        else
        {
            RandomIndex = Random.Range(4, tilePrefabs.Length);
        }
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        Destroy(activeArrows[0]);
        activeTiles.RemoveAt(0);
        activeArrows.RemoveAt(0);
    }
}
