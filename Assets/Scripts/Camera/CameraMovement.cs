using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private float fallingSpeed = 1f;

	public bool isActive = false;

	private void Update()
	{
		if (isActive)
		{
			Vector3 value = Vector3.down * Time.deltaTime * fallingSpeed;
			transform.Translate(value);
		}
	}
}
