using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    FirstPersonController player;

    NavMeshAgent agent; // nav mash dell'agent

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // ottengo la nav mesh dell'robot
        player = FindFirstObjectByType<FirstPersonController>(); // FirstPearsonController del player (per ottenere la posizione del player
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position); // imposto la nuova posizione in cui il robot deve andare
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            HealthManager enemyHealt=GetComponent<HealthManager>();

            enemyHealt.SelfDestruction();
        }
    }
}
