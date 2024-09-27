using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 20f;
	public int damage = 25;
	private Rigidbody2D rigidbody2d;

	void Start()
    {
		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.velocity = transform.right * bulletSpeed;
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();
		FlyingEnemyMove flyingEnemyMove = hitInfo.GetComponent<FlyingEnemyMove>();
		Boss boss = hitInfo.GetComponent<Boss>();
		if (enemy != null)
        {
			enemy.TakeDamage(damage);
        }
		else if(flyingEnemyMove != null)
        {
			flyingEnemyMove.TakeDamage(damage);
        }
		else if(boss != null)
        {
			boss.TakeDamage(damage);
        }
		Destroy(gameObject);
    }

	private void OnBecameVisible()
	{
		rigidbody2d.velocity = transform.right * bulletSpeed;
	}

	private void OnBecameInvisible()
	{
		Invoke("Destroy", 0.25f);
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
