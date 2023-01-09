using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;

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

	public void Init()
	{
		transform.position = Vector3.zero;
		Active(false);
	}

	public void ActiveOnMove()
	{
		Active(true);
	}

	private void Active(bool active)
	{
		rigid.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
		isActive = active;
	}

	private void Jump()
	{
		float x = cam.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? sideForce : -sideForce;
		rigid.velocity = new Vector2(x, upForce);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		gameObject.SetActive(false);
	}
}
