using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLevel : Level
{
	[Space]
	[Header("Setting")]
	[SerializeField] private Transform box;
	[SerializeField] private float distance;
	[SerializeField] private float randomness;

	public override void Initialize()
	{
		
	}

	public override void OnSpawn()
	{
		float x = GameManager.Instance.sideRighted ? -distance : distance;
		x += Random.Range(-randomness, randomness);
		box.transform.position = new Vector3(x, box.transform.position.y);
		GameManager.Instance.sideRighted = !GameManager.Instance.sideRighted;
	}
}
