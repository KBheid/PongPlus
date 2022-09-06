using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public List<GameObject> powerupPrefabs;

    public float powerupMaxFrequency;
    public float powerupMinFrequency;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn a powerup at the beginning
        SpawnPowerup();
        StartCoroutine(nameof(PeriodicSpawn));
    }

    private void SpawnPowerup()
	{
        int powerUpInd = Random.Range(0, powerupPrefabs.Count);
        float xPos = Random.Range(-6f, 6f);
        float yPos = Random.Range(-2f, 2f);

        Instantiate(powerupPrefabs[powerUpInd], new Vector2(xPos, yPos), Quaternion.Euler(0, 0, 0));
    }

    // Periodically spawn powerups
    IEnumerator PeriodicSpawn()
	{
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(powerupMinFrequency, powerupMaxFrequency));
            SpawnPowerup();
        }
	}
}
