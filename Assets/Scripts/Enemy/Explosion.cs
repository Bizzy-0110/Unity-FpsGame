using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Explode();
    }

    private void OnDrawGizmos() // richiamata quando attiviamo i gizmo
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius); // disegno il gizmo per il debug
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius); // controllo quali collider sono all' interno alla sfera di raggio radius

        foreach (Collider collider in colliders)
        {
            PlayerHealt playerHealt = collider.GetComponent<PlayerHealt>();
            if (!playerHealt) continue;

            playerHealt.TakeDamage(damage);

            break; // stoppo il ciclo dato che cìè solo un player
        }
    }

}
