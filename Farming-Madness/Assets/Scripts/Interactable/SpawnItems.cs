using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject[] items;
    public float timeToSpawn;
    public Product product;
    private int spawnedItems = 0;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);

            if (spawnedItems < items.Length) 
            {
                items[spawnedItems].SetActive(true);
                spawnedItems++;
            }
            
            if (spawnedItems == items.Length)
                StopCoroutine("Spawn");
        }
    }

    public void Interact(Crop crop, bool hasCan, PlayerInteraction player)
    {
        if (crop == null && !hasCan && spawnedItems > 0) 
        {
            spawnedItems--;
            items[spawnedItems].SetActive(false);
            StartCoroutine("Spawn");
            player.SetProduct(product);
        }
    }
}
