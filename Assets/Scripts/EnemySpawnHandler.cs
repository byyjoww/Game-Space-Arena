using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemySpawnHandler : MonoBehaviour
{
    //Objects
    [SerializeField] private MapPositions mapPositions;

    private void Start()
    {
        InvokeRepeating("EventStartNotice", firstEventTime - 1.5f, recurringEventTime);
        InvokeRepeating("EventStart", firstEventTime, recurringEventTime);
        Invoke("GetReadyNotice", 0.5f);
        StateMachine.OnGameEnd += GameOver;
    }

    private void OnDestroy()
    {
        //Debug.LogError("Destroyed Enemy Spawn Handler!");
        StateMachine.OnGameEnd -= GameOver;
    }

    private void OnValidate()
    {
        if (mapPositions == null) mapPositions = GetComponent<MapPositions>();
    }

    //Spawn Regulators
    [SerializeField] private int enemyVolume = 2;
    [SerializeField] private int intensity = 1;
    [SerializeField] private float firstEventTime;
    [SerializeField] private float recurringEventTime;

    public void GameOver()
    {
        CancelInvoke();
    }

    private void GetReadyNotice()
    {
        GameObject g = Instantiate(mapPositions.GetReadyPopup, mapPositions.WaveNoticePopupParent);
    }

    private void EventStartNotice()
    {
        GameObject g = Instantiate(mapPositions.WaveNoticePopup, mapPositions.WaveNoticePopupParent);
    }

    private void EventStart()
    {
        SelectEnemyType().Invoke(intensity);
        //Debug.Log("Start Wave: " + intensity);

        IncreaseIntensity();
        IncreaseMForce();
        IncreaseEnemyVolume();
    }

    private Action<int> SelectEnemyType()
    {
        int r = UnityEngine.Random.Range(1, 4);
        //Debug.LogError("Range: " + r);

        if (r == 1) return SpawnLine;
        if (r == 2) return SpawnConverge;
        if (r == 3) return SpawnSpread;

        else throw new System.Exception("invalid RNG roll for enemy type");
    }

    private void IncreaseIntensity()
    {
        intensity += 1;
        if (intensity >= 10) intensity = 10;
        //Debug.Log("intensity = " + intensity);
    }

    private void IncreaseMForce()
    {
        mForce += 2;
        if (mForce >= 80) mForce = 80;
        //Debug.Log("mForce = " + mForce);
    }

    private void IncreaseEnemyVolume()
    {
        enemyVolume += 1;
        if (enemyVolume >= 20) enemyVolume = 20;
        //Debug.Log("enemyVolume = " + enemyVolume);
    }

    //Enemy List
    [SerializeField] private GameObject pfProjectile;
    [SerializeField] private GameObject pfMissile;
    [SerializeField] private GameObject pfLandmine;

    //Spawn Functions
    [SerializeField] private int convergeRadius = 3;
    [SerializeField] private int mForce = 30;

    private void SpawnLine(int intensityAmount)
    {
        List<Vector2> CurrentSpawnLocations = new List<Vector2>();
        string spawnDirection = GetSpawnDirection(intensityAmount);
        PopulateSpawnList(CurrentSpawnLocations, spawnDirection);
        InstantiateLineEnemies(CurrentSpawnLocations, spawnDirection);
    }

    private Dictionary<int, string> DirectionDictionary = new Dictionary<int, string>()
    {
        { 1, "right" },
        { 2, "left" },
        { 3, "right" },
        { 4, "left" },
        { 5, "top" },
        { 6, "bottom" },
        { 7, "right" },
        { 8, "left" },
        { 9, "right" },
        { 10, "left" },
        { 11, "top" },
        { 12, "bottom" },
    };

    private string GetSpawnDirection(int intensityAmount)
    {
        string spawnDirection;

        int r = UnityEngine.Random.Range(1, intensityAmount);

        spawnDirection = DirectionDictionary[r];

        //Debug.LogError("Spawn Location: " + spawnDirection);
        return spawnDirection;
    }

    private Vector2 SpawnVector(string direction, int k)
    {
        Vector2 v;

        if (direction == "right")
        {
            v = new Vector2(mapPositions.X_max, mapPositions.Y_min + ((mapPositions.Y_max - mapPositions.Y_min) * (float)k / (float)enemyVolume));
        }

        else if (direction == "left")
        {
            v = new Vector2(mapPositions.X_min, mapPositions.Y_min + ((mapPositions.Y_max - mapPositions.Y_min) * (float)k / (float)enemyVolume));
        }

        else if (direction == "top")
        {
            v = new Vector2(mapPositions.X_min + (mapPositions.X_max - mapPositions.X_min) * (float)k / (float)enemyVolume, mapPositions.Y_max);
        }

        else if (direction == "bottom")
        {
            v = new Vector2(mapPositions.X_min + (mapPositions.X_max - mapPositions.X_min) * (float)k / (float)enemyVolume, mapPositions.Y_min);
        }

        else
        {
            throw new System.Exception("Invalid Direction");
        }

        return v;
    }

    private void PopulateSpawnList(List<Vector2> CurrentSpawnLocations, string spawnDirection)
    {
        for (int k = 0; k < enemyVolume; k++)
        {
            CheckValidCoordinates(CurrentSpawnLocations, SpawnVector(spawnDirection, k));
        }
    }

    private void CheckValidCoordinates(List<Vector2> CurrentSpawnLocations, Vector2 v)
    {
        if (v.x < (mapPositions.X_max + 0.1f) && v.x > (mapPositions.X_min - 0.1f) && v.y < (mapPositions.Y_max + 0.1f) && v.y > (mapPositions.Y_min - 0.1f))
        {
            CurrentSpawnLocations.Add(v);
        }
        else
        {
            //Debug.LogError("Invalid Coordinates! " + v);
        }
    }

    private void SpawnConverge(int intensityAmount)
    {
        List<Vector2> CurrentSpawnLocations = new List<Vector2>();

        for (int k = 0; k <= enemyVolume; k++)
        {
            Vector2 v = new Vector2(convergeRadius * Mathf.Cos(2f * Mathf.PI * (float)k / (float)enemyVolume) + mapPositions.Player.position.x, convergeRadius * Mathf.Sin(2f * Mathf.PI * (float)k / (float)enemyVolume) + mapPositions.Player.position.y);
            CheckValidCoordinates(CurrentSpawnLocations, v);
        }

        InstantiateConvergeEnemies(CurrentSpawnLocations);
    }

    private void SpawnSpread(int intensityAmount)
    {
        List<Vector2> CurrentSpawnLocations = new List<Vector2>();

        for (int k = 1; k <= enemyVolume; k++)
        {
            float x = UnityEngine.Random.Range(mapPositions.X_min, mapPositions.X_max);
            float y = UnityEngine.Random.Range(mapPositions.Y_min, mapPositions.Y_max);
            Vector2 v = new Vector2(x, y);
            CheckValidCoordinates(CurrentSpawnLocations, v);
        }

        InstantiateSpreadEnemies(CurrentSpawnLocations);
    }

    private void InstantiateLineEnemies(List<Vector2> CurrentSpawnLocations, string spawnLocation)
    {
        foreach (Vector2 i in CurrentSpawnLocations)
        {
            GameObject prefab = Instantiate(pfProjectile, i, Quaternion.identity);
            EnemyMovement prefabScript = prefab.GetComponent<EnemyMovement>();
            if (spawnLocation == "right") prefabScript.movementForce = new Vector2(-mForce, 0);
            else if (spawnLocation == "left") prefabScript.movementForce = new Vector2(mForce, 0);
            else if (spawnLocation == "top") prefabScript.movementForce = new Vector2(0, -mForce);
            else if (spawnLocation == "bottom") prefabScript.movementForce = new Vector2(0, mForce);
        }
    }

    private void InstantiateConvergeEnemies(List<Vector2> CurrentSpawnLocations)
    {
        foreach (Vector2 i in CurrentSpawnLocations)
        {
            GameObject prefab = Instantiate(pfMissile, i, Quaternion.identity);
            EnemyMovement prefabScript = prefab.GetComponent<EnemyMovement>();
            prefabScript.speed = prefabScript.speed + (float)intensity * 0.05f;
        }
    }

    private void InstantiateSpreadEnemies(List<Vector2> CurrentSpawnLocations)
    {
        foreach (Vector2 i in CurrentSpawnLocations)
        {
            Instantiate(pfLandmine, i, Quaternion.identity);
        }
    }
}