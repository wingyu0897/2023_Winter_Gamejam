using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
	private SpriteRenderer spriteRenderer;
	private TrailRenderer trailRenderer;
	private ScoreSystem scoreSystem;

	[Header("Effect")]
	public ParticleSystem deathEffect;

	[Header("Movement")]
	[SerializeField] private float upForce;
	[SerializeField] private float sideForce;

	private bool isActive = false;

	private Camera cam;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		trailRenderer = GetComponent<TrailRenderer>();
		cam = Camera.main;
	}

	private void Update()
	{
		if (isActive)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Jump();
			}
		}
	}

	#region #Movement
	public void ActiveMove(bool active)
	{
		rigid.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
		isActive = active;
		trailRenderer.enabled = active;
	}

	private void Jump()
	{
		float x = cam.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? sideForce : -sideForce;
		rigid.velocity = new Vector2(x, upForce);
	}
	#endregion

	public void Init()
	{
		transform.position = Vector3.zero;
		spriteRenderer.enabled = true;
		scoreSystem = GameManager.Instance.GetSystem<ScoreSystem>() as ScoreSystem;
		ActiveMove(false);
		deathEffect.Clear();
	}

	private void Die()
	{
		spriteRenderer.enabled = false;
		deathEffect.Play();
		ActiveMove(false);
		GameManager.Instance.UpdateState(GameState.Result);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("DeathWall"))
		{
			Die();
		}
		else if (collision.CompareTag("Score"))
		{
			scoreSystem.AddScore();
			collision.enabled = false;
		}
	}
}
