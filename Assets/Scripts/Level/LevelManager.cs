using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelPrefab
{
	public Level prefab;
	public int appearPoint = 0;
	public int disAppearPoint = -1;
}

[RequireComponent(typeof(LevelPoolManager))]
public class LevelManager : MonoBehaviour
{
	[Header("<Pool>")]
	[SerializeField]
	private PoolLevelList poolList;
	private LevelPoolManager poolManager;

	[Header("<Levels>")]
	[SerializeField]
	[Tooltip("스폰될 레벨들")]
	private List<LevelPrefab> levelPrefabs;
	[SerializeField]
	[Tooltip("스폰 불가능한 레벨들")]
	private List<LevelPrefab> unUsableLevels;
	[SerializeField]
	[Tooltip("현재 스폰 가능한 레벨들")]
	private List<LevelPrefab> usableLevels;
	[SerializeField]
	[Tooltip("현재 스폰된 레벨들")]
	private List<Level> spawnedLevels = new List<Level>();
	[SerializeField]
	[Tooltip("가장 최근 스폰된 레벨")]
	private Level lateInput = null;

	[Header("<Spawning>")]
	public bool isRunning = false;
	[SerializeField]
	private int weight = 0;

	[Header("<Setting>")]
	[SerializeField]
	private Vector3 spawnOffset;
	public Vector3 spawnPos;
	private Vector2 topPos;

	private Camera cam;

	private void Awake()
	{
		poolManager = GetComponent<LevelPoolManager>();
		cam = Camera.main;
		topPos = Vector3.zero - cam.ScreenToWorldPoint(new Vector3(0.5f, 0));
	}

	private void Start()
	{
		foreach (Pool pl in poolList.list)
		{
			poolManager.CreatePool(pl.prefab, transform, pl.count);
		}
	}

	private void Update()
	{
		if (isRunning)
		{
			spawnPos = cam.transform.position + spawnOffset;

			CheckAppearPoint();
			CheckDisAppearPoint();
			SpawnLevels();
			PushLevels();
		}
	}
	/// <summary>
	/// 레벨을 스폰하는 함수.
	/// </summary>
	private void SpawnLevels()
	{
		if (usableLevels.Count == 0)
			return;

		Level obj;

		if (lateInput != null)
		{
			if (lateInput.transform.position.y - spawnPos.y >= lateInput.lenght)
			{
				int weightValue = Random.Range(0, weight + 1);
				obj = Pop(WeightToPrefab(weightValue));
				obj.transform.position = spawnPos;
			}
		}
		else
		{
			obj = Pop(usableLevels[0].prefab);
			obj.transform.position = spawnPos;
		}
	}
	/// <summary>
	/// usableLevels의 prefab의 maxWeight를 확인하며 해당하는 prefab을 반환한다.
	/// </summary>
	/// <param name="weight">가중치</param>
	/// <returns>usableLevels의 prefab에서 매개변수 값에 일치하는 prefab</returns>
	private Level WeightToPrefab(int weight)
	{
		for (int i = 0; i < usableLevels.Count; i++)
		{
			if (weight <= usableLevels[i].prefab.maxWeight)
			{
				return usableLevels[i].prefab;
			}
		}
		Debug.LogWarning($"해당 가중치 <{weight}>는 usableLevels의 모든 가중치를 넘어섭니다.");
		return usableLevels[0].prefab;
	}
	/// <summary>
	/// 화면 밖으로 나간 레벨을 회수하는 함수.
	/// </summary>
	private void PushLevels()
	{
		if (spawnedLevels.Count > 0)
		{
			Level firLevel = spawnedLevels[0];

			float topPosY = topPos.y + cam.transform.position.y;

			if (firLevel.transform.position.y - topPosY - 1 >= firLevel.lenght)
			{
				Push(firLevel);
			}
		}
	}
	/// <summary>
	/// 스폰할 레벨들에서 출현할 포인트(스코어)를 만족하면 스폰 가능한 리스트로 옮기는 함수.
	/// </summary>
	private void CheckAppearPoint()
	{
		if (unUsableLevels.Count > 0)
		{
			int count = unUsableLevels.Count;
			for (int i = 0; i < count; i++)
			{
				if (unUsableLevels[0].appearPoint <= GameManager.Instance.score)
				{
					usableLevels.Add(unUsableLevels[0]);
					weight += unUsableLevels[0].prefab.weight; //가중치 값 합산
					unUsableLevels[0].prefab.maxWeight = weight; //최대 가중치 값 지정
					unUsableLevels.RemoveAt(0);
				}
			}
		}
	}

	private void CheckDisAppearPoint()
	{
		if (usableLevels.Count > 0)
		{
			int count = usableLevels.Count;
			int index = 0;
			for (int i = 0; i < count; i++)
			{
				if (usableLevels[index].disAppearPoint > 0 && usableLevels[index].disAppearPoint <= GameManager.Instance.score)
				{
					weight -= usableLevels[index].prefab.weight;
					usableLevels.RemoveAt(index);
				}
				else
				{
					index++;
				}
			}
		}
	}

	public void Active()
	{
		isRunning = true;
	}

	/// <summary>
	/// 초기화 함수.<br/>
	/// 스폰된 레벨들을 회수한다.
	/// </summary>
	public void Initialize()
	{
		isRunning = false;

		int count = spawnedLevels.Count;
		for (int i = 0; i < count; i++)
		{
			Push(spawnedLevels[0]);
		}

		spawnPos = spawnOffset;
		levelPrefabs = levelPrefabs.OrderBy(i => i.appearPoint).ToList(); //levelPrefbs.appearPoint 값을 기준으로 내림차순 정렬
		usableLevels.Clear(); //사용가능한 레벨들 초기화
		unUsableLevels = levelPrefabs.ToList(); //사용 불가능한 레벨들 초기화(스폰할 레벨들에서 값 복사)

		weight = 0;
		lateInput = null;

		GameManager.Instance.sideRighted = false;
	}

	public Level Pop(Level level)
	{
		Level obj = poolManager.Pop(level);
		spawnedLevels.Add(obj);
		lateInput = obj;
		obj.OnSpawn();
		return obj;
	}

	public void Push(Level obj)
	{
		obj.Initialize();
		spawnedLevels.Remove(obj);
		poolManager.Push(obj);
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(spawnPos, 0.5f);
		Gizmos.color = Color.white;
	}
#endif
}
