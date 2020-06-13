using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCommandCenter : MonoBehaviour
{
    [SerializeField] List<MissileController> missileList = new List<MissileController>();
    List<Transform> targetList = new List<Transform>();

    private void Start()
    {
        GetMissiles();
        AssignTargets();
    }

    private void GetMissiles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            missileList[i] = transform.GetChild(i).GetComponent<MissileController>();
        }
    }

    private void AssignTargets()
    {
        foreach (var missile in missileList)
        {
            var enemy = FindClosestEnemy(targetList);
            missile.SetTarget(enemy);
            targetList.Add(enemy);
        }
    }

    private Transform FindClosestEnemy(List<Transform> selectedTargets)
    {
        EnemyMovement[] allEnemies = FindObjectsOfType<EnemyMovement>();

        Transform closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (EnemyMovement enemy in allEnemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                if (selectedTargets.Contains(enemy.transform) == false)
                {
                    closest = enemy.transform;
                    distance = curDistance;
                }            
            }
        }

        return closest;
    }
}
