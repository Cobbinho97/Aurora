using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float playerSpeed = 30f;
	public float jumpingSpeed;
	public Transform shootingPoint;
	public GameObject bulletObject;
	public GameObject blackHoleObject;
	public float hurtCounter;
	public float shootingCounter;
	public AudioSource jumpSound;
	public AudioSource pickupSound;
	public AudioSource jetpackSound;
	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;
	public GameObject speedPickUpEffect;
	public int maxAmmo = 20;
	private int currentAmmo;
	public float reloadTime = 1f;
	private bool isReloading = false;
	public Text ammoText;
	public Transform keyFollowPoint;
	public Key followingKey;
	public ParticleSystem ps;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;

	private Rigidbody2D playerRigidbody;
	private Animator playerAnimator;
	private bool isPlayerOnGround;
	public bool jetpack;
	public bool getJetpack;
	private float aHurtCounter;
	private float aShootingCounter;
	private bool facingRight;
	public int bulletsAmount = 20;
	private int bulletIndex;
	private WaitForSeconds wait;

	private readonly int playerSpeedID = Animator.StringToHash("PlayerSpeed");
	private readonly int onGroundID = Animator.StringToHash("OnGround");
	private readonly int teleporID = Animator.StringToHash("Teleport");
	private readonly int hurtID = Animator.StringToHash("Hurt");
	private readonly int shootingID = Animator.StringToHash("Shoot");
	private readonly int shootingOnAirID = Animator.StringToHash("ShootOnAir");
	private readonly int isShootingID = Animator.StringToHash("IsShooting");
	private readonly int skillAttackID = Animator.StringToHash("SkillAttack");

	void Start()
    {
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
		facingRight = true;
		wait = new WaitForSeconds(1.5f);
		jumpSound = GetComponent<AudioSource>();
		pickupSound = GetComponent<AudioSource>();
		jetpackSound = GetComponent<AudioSource>();
		gravityStore = playerRigidbody.gravityScale;
		getJetpack = false;
		currentAmmo = maxAmmo;
		ps = GetComponentInChildren<ParticleSystem>();
	}

	void Update()
	{
		isPlayerOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		if (aHurtCounter <= 0f)
		{
			#region Horizontal Movement
			if (Input.GetAxisRaw("Horizontal") > 0f)
			{
				playerRigidbody.velocity = new Vector3(playerSpeed, playerRigidbody.velocity.y, 0f);

				if (!facingRight)
				{
					FlipPlayer();
				}
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f)
			{
				playerRigidbody.velocity = new Vector3(-playerSpeed, playerRigidbody.velocity.y, 0f);

				if (facingRight)
				{
					FlipPlayer();
				}
			}
			else
			{
				playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
			}
			#endregion

			#region Vertical Movement (Jump)
			if ((Input.GetKeyDown(KeyCode.UpArrow)) && isPlayerOnGround)
			{
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpingSpeed, 0f);
				jumpSound.Play();
			}
			if (getJetpack == true && Input.GetKeyDown(KeyCode.UpArrow))
			{
				{
					playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpingSpeed, -1f);
					ps.Play();
					jetpackSound.Play();
					jetpack = true;
				}
			}
			#endregion
		}
		else
		{
			aHurtCounter -= Time.deltaTime;
		}

		AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
		playerAnimator.SetBool(onGroundID, isPlayerOnGround);
		playerAnimator.SetFloat(playerSpeedID, Mathf.Abs(playerRigidbody.velocity.x));

		if (aShootingCounter <= 0f)
		{
			playerAnimator.SetBool(isShootingID, false);
		}
		else
		{
			aShootingCounter -= Time.deltaTime;
		}

		#region Teleport
		if (Input.GetKeyDown(KeyCode.T) && isPlayerOnGround)
		{
			if (Mathf.Abs(playerRigidbody.velocity.x) < 0.05f)
				playerAnimator.SetTrigger(teleporID);
		}
		#endregion

		#region Hurt
		else if (Input.GetKeyDown(KeyCode.H))
		{
			playerAnimator.SetTrigger(hurtID);
			if (Mathf.Abs(playerRigidbody.velocity.x) != 0.05f)
			{
				playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
				aHurtCounter = hurtCounter;
			}
		}
		#endregion

		if (isReloading)
		{
			return;
		}
		if (currentAmmo <= 0)
		{
			StartCoroutine(Reload());
			return;
		}

		else if (Input.GetKeyDown(KeyCode.Space))
		{
			playerAnimator.SetBool(isShootingID, true);
			currentAmmo--;
			ammoText.text = currentAmmo.ToString();
			if (isPlayerOnGround)
			{
				shootingPoint.position = new Vector3(shootingPoint.position.x, transform.position.y - 0.02f, shootingPoint.position.z);
				if (Math.Abs(playerRigidbody.velocity.x) < 0.05f)
				{
					playerAnimator.SetTrigger(shootingID);
				}
				else
				{
					playerAnimator.SetBool(isShootingID, true);
					aShootingCounter = shootingCounter;
				}
			}
			else
			{
				shootingPoint.position = new Vector3(shootingPoint.position.x, transform.position.y + 0.22f, shootingPoint.position.z);
				playerAnimator.SetTrigger(shootingOnAirID);
			}

			Shoot();
		}

		if (onLadder)
		{
			playerRigidbody.gravityScale = 0f;
			climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
			playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, climbVelocity);
		}
		if (!onLadder)
		{
			playerRigidbody.gravityScale = 8f;
		}
	}

	private void FlipPlayer()
	{
		facingRight = !facingRight; 
		transform.Rotate(0f, 180f, 0f);
	}

	private void Shoot()
	{
		Instantiate(bulletObject, shootingPoint.position, shootingPoint.rotation);
	}

	public IEnumerator Reload()
    {
		isReloading = true;
		yield return new WaitForSeconds(reloadTime);
		currentAmmo = maxAmmo;
		isReloading = false;
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SpeedPickUp")
		{
			Instantiate(speedPickUpEffect, transform.position, transform.rotation);

			pickupSound.Play();

			playerSpeed = 40;

			Destroy(other.gameObject);

			StartCoroutine(ResetSpeedPowerUp());
		}
		if (other.tag == "SidewaysPlatform")
		{
			transform.parent = other.gameObject.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
    {
		transform.parent = null;
    }

	private IEnumerator ResetSpeedPowerUp()
    {
		yield return new WaitForSeconds(8);
		playerSpeed = 30;
    }
}
