using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerPowerup : MonoBehaviour
{
	[SerializeField] List<GameObject> shapePrefabs;
	[SerializeField] float duration = 10f;

	private GameObject blocker;
	private bool playerHitBall;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PongBall pb))
		{
			playerHitBall = pb.playerLastHit;

			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;

			StartCoroutine(nameof(CreateAndDestroyAfterTime));
		}

	}

	IEnumerator CreateAndDestroyAfterTime()
	{
		yield return new WaitForSeconds(1.25f);
		Quaternion qt = (playerHitBall) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
		blocker = Instantiate(shapePrefabs[Random.Range(0, shapePrefabs.Count)], transform.position, qt);

		yield return new WaitForSeconds(duration);
		Destroy(blocker);
		Destroy(gameObject);
	}
}
