using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject entityPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval=5f;

    PlayerHealt playerHealt;


    void Start()
    {
        playerHealt = FindFirstObjectByType<PlayerHealt>();
        StartCoroutine(SpawnCoroutine());

    }

    IEnumerator SpawnCoroutine() // thread a parte
    {
        while (playerHealt)
        {
            Instantiate(entityPrefab, spawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(spawnInterval); // aspetto per istanziare
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
