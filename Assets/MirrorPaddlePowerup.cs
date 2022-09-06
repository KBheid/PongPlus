using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPaddlePowerup : MonoBehaviour
{
	public float powerupDuration = 10f;

	GameObject mirroredObject;
	GameObject addedPaddle;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PongBall pb))
		{
			mirroredObject = (pb.playerLastHit) ? Game.instance.PlayerPaddle : Game.instance.CPUPaddle;

			addedPaddle = Instantiate(mirroredObject);

			// Disable paddle controller
			if (addedPaddle.TryGetComponent(out PlayerPaddle playerPaddleScript))
				playerPaddleScript.enabled = false;
			if (addedPaddle.TryGetComponent(out CPUPaddle cpuPaddleScript))
				cpuPaddleScript.enabled = false;

			StartCoroutine(nameof(MoveNewPaddle));

			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<Collider2D>().enabled = false;
		}
	}

	IEnumerator MoveNewPaddle()
	{
		float curDuration = 0f;
		while (curDuration < powerupDuration)
		{
			addedPaddle.transform.position = new Vector2(mirroredObject.transform.position.x, -mirroredObject.transform.position.y);

			yield return new WaitForSeconds(0.035f);
			curDuration += 0.035f;
		}
		Destroy(addedPaddle);
		yield return new WaitForSecondsRealtime(0.1f);
		Destroy(gameObject);
	}
}
