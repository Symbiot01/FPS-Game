using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int maxTargets = 4;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    
    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        // Initial spawn of targets
        for (int i = 0; i < maxTargets; i++)
        {
            SpawnTarget();
        }
    }

    public void SpawnTarget()
    {
        if (targets.Count >= maxTargets)
            return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        targets.Add(newTarget);
    }

    public void TargetDestroyed(GameObject target)
    {
        targets.Remove(target);
        SpawnTarget();
    }
}   