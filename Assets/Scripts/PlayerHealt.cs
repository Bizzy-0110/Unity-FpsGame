using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{

    [SerializeField] int startingHealt = 5;
    [SerializeField] Cinemachine.CinemachineVirtualCamera deathVcam;
    [SerializeField] Transform weaponCamera;

    [SerializeField] Image[] shieldBar;

    int currrentHealt;

    private void UpdateShieldBar()
    {
        for (int i = 0; i < shieldBar.Length; i++)
        {
            shieldBar[i].enabled = i<currrentHealt;
        }
    }

    private void Awake()
    {
        currrentHealt = startingHealt;

    }

    public void TakeDamage(int damage)
    {
        currrentHealt -= damage;

        UpdateShieldBar(); // aggiorno la barra della vita

        if (currrentHealt <= 0)
        {
            weaponCamera.parent = null; // rimuovo la telecamera
            deathVcam.Priority = 20;
            
            Destroy(gameObject); // temporaneo
        }
    }
}
