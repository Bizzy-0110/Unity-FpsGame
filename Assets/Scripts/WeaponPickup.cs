using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO; // arma del pickup

    const string PLAY_TAG = "Player";

    private void OnTriggerEnter(Collider other) // evento di entrata nel collider
    {
        if(other.CompareTag(PLAY_TAG)) // controllo se è il player
        {
            ActiveWeapon aw = other.GetComponentInChildren<ActiveWeapon>();

            aw.SwitchWeapon(weaponSO); // cambio l'arma attiva

            Destroy(gameObject); // distruggo il pickup
        }
    }
}
