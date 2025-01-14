using UnityEngine;

public class PlayerHealt : MonoBehaviour
{

    [SerializeField] int startingHealt = 5;
    [SerializeField] Cinemachine.CinemachineVirtualCamera deathVcam;
    [SerializeField] Transform weaponCamera;

    int currrentHealt;

    private void Awake()
    {
        currrentHealt = startingHealt;

    }

    public void TakeDamage(int damage)
    {
        currrentHealt -= damage;

        if (currrentHealt <= 0)
        {
            weaponCamera.parent = null; // rimuovo la telecamera
            deathVcam.Priority = 20;
            
            Destroy(gameObject); // temporaneo
        }
    }
}
