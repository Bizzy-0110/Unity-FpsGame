using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;

    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<FirstPersonController>();
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
