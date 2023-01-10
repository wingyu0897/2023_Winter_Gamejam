using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;
	private ScoreSystem scoreSystem;
	public GameObject deathEffectobj;

	[SerializeField] private float upForce;
	[SerializeField] private float sideForce;

	private bool isActive = false;

	private Camera cam;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
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
	}

	private void Jump()
	{
		float x = cam.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? sideForce : -sideForce;
		rigid.velocity = new Vector2(x, upForce);
	}
	#endregion

	public void Init()
	{
		ActiveMove(false);
		gameObject.SetActive(true);
		transform.position = Vector3.zero;
		scoreSystem = GameManager.Instance.GetSystem<ScoreSystem>() as ScoreSystem;
	}

	private void Die()
	{
		Instantiate(deathEffectobj,transform.position,Quaternion.identity);

		ActiveMove(false);
		gameObject.SetActive(false);
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
