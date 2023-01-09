using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    [Header("LevelSetting")]
    public float lenght;

    [HideInInspector]
    public int maxWeight = 0;
    public int weight;

    public abstract void Initialize();
    public abstract void OnSpawn();
}
