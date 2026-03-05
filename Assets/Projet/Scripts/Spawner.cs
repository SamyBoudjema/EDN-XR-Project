using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;
    public float spawnForceMin = 5f;
    public float spawnForceMax = 10f;
    public float spawnTorqueMax = 5f;
    public Transform spawnArea; 
    
    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart.AddListener(StartSpawning);
            GameManager.Instance.OnGameOver.AddListener(StopSpawning);
        }
    }

    public void StartSpawning()
    {
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            if (prefabsToSpawn.Count > 0)
            {
                GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
                
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.2f, 0.2f));
                
                GameObject spawned = Instantiate(prefab, spawnPos, Random.rotation);
                
                Rigidbody rb = spawned.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    float force = Random.Range(spawnForceMin, spawnForceMax);
                    rb.AddForce(Vector3.up * force, ForceMode.Impulse);
                    
                    // On désactive la rotation automatique (Torque) pour que les fruits ne tournoient plus
                    // rb.AddTorque(Random.insideUnitSphere * spawnTorqueMax, ForceMode.Impulse);

                    // Option : Ralentir la chute en augmentant la résistance de l'air (Drag)
                    // Plus le chiffre est élevé (ex: 3 au lieu de 0), plus le fruit flottera et tombera lentement.
                    rb.drag = 3f;
                }

                // Détruire l'objet après 3 secondes s'il n'a pas été coupé pour libérer la mémoire.
                Destroy(spawned, 3f);
            }
        }
    }
}
