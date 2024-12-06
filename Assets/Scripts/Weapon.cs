using UnityEngine;

public class Weapon : MonoBehaviour
{
    ExtendedStarterAssetsInputs extendedStarterAssetsInputs;

    [SerializeField] int WeaponDamage = 1; // danno dell'arma per colpo

    [SerializeField] ParticleSystem shotEffect; // effetto di sparo
    [SerializeField] Animator animator; // controller per l'animazione

    [SerializeField] GameObject hitVFXPrefab;

    const string SHOOT_STRING = "Shoot"; // nome dell' animazione di sparo

    private void Awake() // funzione che viene eseguata ancora prima della start()
    {
        extendedStarterAssetsInputs = GetComponentInParent<ExtendedStarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }


    private void HandleInput() // gestisco il click del pulsante per sparare
    {
        if (!extendedStarterAssetsInputs.shoot) return; // controllo se sparo

        // eseguo l'effetto di sparo
        shotEffect.Play();

        // avvio l'animazione di sparo
        animator.Play(SHOOT_STRING, 0, 0);

        RaycastHit hit; // raggio invisibile

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)) // lancio il raggio invisibile e controllo se ha colpito qualcosa
        {
            Instantiate(hitVFXPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            Debug.Log(hit.collider.gameObject.name); // mostro a console il nome dell' oggettro colpito

            HealtManager healtManage = hit.collider.GetComponent<HealtManager>(); // ottengo l'HealtManager dell'oggetto colpito

            healtManage?.TakeDamage(WeaponDamage); // se l'oggetto ha lo script healt manager gli infliggo il danno

        }

        extendedStarterAssetsInputs.ShootInput(false); // resetto il flag dello sparo

    }
}
