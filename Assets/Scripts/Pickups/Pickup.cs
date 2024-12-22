using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;// velocità di rotazione del pickup

    [SerializeField] protected bool destroyOnPickup = true; // distruggo il pickup dopo averlo raccolto

    const string PLAY_TAG = "Player"; // tag che può raccogliere i pickups

    private void OnTriggerEnter(Collider other) // evento di entrata nel collider
    {
        
        if (other.CompareTag(PLAY_TAG)) // controllo se è il player
        {
            ActiveWeapon aw = other.GetComponentInChildren<ActiveWeapon>();

            OnPickup(aw);

            if (destroyOnPickup) // controllo se devo distruggere il pickup
                Destroy(gameObject); // distruggo il pickup
        }
    }

    protected void rotateModel() // metodo per l'animazione di rotazione del modello
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed); // ruoto il modello
    }

    protected abstract void OnPickup(ActiveWeapon aw);

}
