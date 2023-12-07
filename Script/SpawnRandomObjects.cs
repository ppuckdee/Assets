using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObjects : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array of your object prefabs
    public int maxNumberOfObjects = 6;
    public Terrain terrain;
    public float aboveGroundOffset = 1.5f; // Adjust this value to set the height above the terrain
    public Transform objectPool; // Reference to the empty GameObject for object pooling

    void Start()
    {
        if (objectPrefabs == null || objectPrefabs.Length == 0 || terrain == null || objectPool == null)
        {
            Debug.LogError("Prefab array, Terrain, or ObjectPool not assigned!");
            return;
        }

        SpawnObjects();
    }

    void SpawnObjects()
    {
        TerrainData terrainData = terrain.terrainData;

        for (int i = 0; i < maxNumberOfObjects; i++)
        {
            float randX = Random.Range(0f, terrainData.size.x);
            float randZ = Random.Range(0f, terrainData.size.z);
            Vector3 randPos = new Vector3(randX, 0f, randZ);

            // Get the terrain height at the random position
            float terrainHeight = terrain.SampleHeight(randPos);

            // Adjust Y position to place the object above the terrain
            Vector3 spawnPos = new Vector3(randPos.x, terrainHeight + aboveGroundOffset, randPos.z);

            // Randomly select an object from the array of prefabs
            GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

            // Instantiate the object as a child of the objectPool GameObject
            GameObject newObj = Instantiate(selectedPrefab, spawnPos, Quaternion.identity, objectPool);

            // Ensure the object is upright
            newObj.transform.rotation = Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f);
        }
    }
}
