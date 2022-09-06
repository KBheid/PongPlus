using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBallPowerup : MonoBehaviour
{
	public int maxBallsSpawned;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Only execute powerup if the trigger is a ball
		if (!collision.TryGetComponent(out PongBall pb))
			return;

		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;

		StartCoroutine(nameof(PeriodicSpawnBall));
	}

	IEnumerator PeriodicSpawnBall()
	{
		int ballsToSpawn = Random.Range(1, maxBallsSpawned+1);
		int ballsSpawned = 0;

		while (ballsSpawned < ballsToSpawn)
		{
			yield return new WaitForSeconds(0.75f);
			Game.instance.SpawnNewBallImmediate(transform.position);
			ballsSpawned++;
		}

		Destroy(gameObject);
	}
}
