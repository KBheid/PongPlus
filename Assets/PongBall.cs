using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{

    [Tooltip("The amount that a non-perfect hit will change the Y velocity of the ball.")]
    [SerializeField] float yOffsetMultiplier;
    Rigidbody2D _rigidbody;
    AudioSource _audioSource;

    public bool playerLastHit = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        float xForce = Random.Range(-35f, 35f);
        float yForce = Random.Range(-15f, 15f);

        // Add a random velocity a random direction
        _rigidbody.AddForce(new Vector2(xForce, yForce));
    }

    // Update is called once per frame
    void Update()
    {
        // Add minimum X velocity
        if (Mathf.Abs(_rigidbody.velocity.x) < 1.5f)
		{
            float sign = _rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x);
            if (_rigidbody.velocity.x == 0)
                sign = 1;

            Vector2 newVelocity = new Vector2(sign * 1.5f, _rigidbody.velocity.y);
            _rigidbody.velocity = newVelocity;
        }

        // Cap X velocity
        if (Mathf.Abs(_rigidbody.velocity.x) > 25f)
		{
            float sign = _rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x);
            if (sign == 0)
                sign = 1;

            Vector2 newVelocity = new Vector2(sign * 25f, _rigidbody.velocity.y);

            _rigidbody.velocity = newVelocity;
        }

        // Cap Y velocity
        if (Mathf.Abs(_rigidbody.velocity.y) > 25f)
        {
            float sign = _rigidbody.velocity.y / Mathf.Abs(_rigidbody.velocity.y);
            if (sign == 0)
                sign = 1;

            Vector2 newVelocity = new Vector2(_rigidbody.velocity.x, sign * 25f);

            _rigidbody.velocity = newVelocity;
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        
        if (collision.gameObject.tag == "Paddle")
		{
            Vector2 curPos = collision.transform.position;
            float yChange = (curPos - collision.GetContact(0).point).y * -1;

            _rigidbody.AddForce(new Vector2(0, yChange*yOffsetMultiplier));
            playerLastHit = collision.GetContact(0).point.x < 0;

            _audioSource.Play();
		}

        if (collision.gameObject.tag == "Goal")
		{
            bool goalAgainstCPU = collision.GetContact(0).point.x > 0;

            Game.instance.AddScore(goalAgainstCPU);
        }
	}
}
