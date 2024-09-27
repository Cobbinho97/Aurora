using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
	public float bulletSpeed = 20f;
	public int damage = 25;
	private Transform player;
	private Vector2 target;
	private Player player1;
	private Rigidbody2D rigidbody2d;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = new Vector2(player.position.x, player.position.y);
		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.velocity = -transform.right * bulletSpeed;
	}

	void Update()
	{
		transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);

		if (transform.position.x == target.x && transform.position.y == target.y)
		{
			Destroy(gameObject);
		}
		Destroy(gameObject, 2f);
	}

	private void OnBecameVisible()
	{
		rigidbody2d.velocity = -transform.right * bulletSpeed;
	}

	private void OnBecameInvisible()
	{
		Invoke("Destroy", 0.5f);
	}

	private void Destroy()
	{
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		CancelInvoke();
	}
}