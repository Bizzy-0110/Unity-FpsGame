using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] float radius = 1.5f;

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
        //todo
    }

}
