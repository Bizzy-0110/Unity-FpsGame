using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealt : HealthManager
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera deathVcam;
    [SerializeField] Transform weaponCamera;

    [SerializeField] Image[] shieldBar;


    private void UpdateShieldBar()
    {
        for (int i = 0; i < shieldBar.Length; i++)
        {
            shieldBar[i].enabled = i<currentHealth;
        }
    }

    public override void onDeath() // metodo che viene chiamato quando la salute arriva a zero
    {
        weaponCamera.parent = null; // rimuovo la telecamera
        deathVcam.Priority = 20; // attivo la telecamera della morte

        Destroy(gameObject); // elimino il player (temporaneo)
    }

    public override void onHealtUpdate() // metodo che viene chiamato quando la salute viene aggiornata
    {
        UpdateShieldBar(); // aggiorno la barra della vita
    }
}
