using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    [Header("LevelSetting")]
    public float lenght;

    protected Collider2D scoreCollider;

    [HideInInspector]
    public int maxWeight = 0;
    public int weight;

    public abstract void Initialize();
    public abstract void OnSpawn();

    public virtual void Awake()
	{
        scoreCollider = GetComponentInChildren<EdgeCollider2D>();
	}

    public virtual void ScoreCheckLine()
	{
        scoreCollider.enabled = true;
	}
}
