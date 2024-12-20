using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    const string PLAY_TAG = "Player"; // tag che pu� raccogliere i pickups

    private void OnTriggerEnter(Collider other) // evento di entrata nel collider
    {
        if (other.CompareTag(PLAY_TAG)) // controllo se � il player
        {
            ActiveWeapon aw = other.GetComponentInChildren<ActiveWeapon>();

            OnPickup(aw);

            Destroy(gameObject); // distruggo il pickup
        }
    }

    protected abstract void OnPickup(ActiveWeapon aw);

}
