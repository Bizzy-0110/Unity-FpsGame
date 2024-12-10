using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    Weapon currentWeapon;

    ExtendedStarterAssetsInputs extendedStarterAssetsInputs; // estensione della classe StarterAssetsInputs

    [SerializeField] WeaponSO weaponConf; // Weapon Scriptable Object contenente le impostazioni dell'arma

    Animator animator; // controller per l'animazione

    const string SHOOT_STRING = "Shoot"; // nome dell' animazione di sparo

    private void Awake() // funzione che viene eseguata ancora prima della start()
    {
        extendedStarterAssetsInputs = GetComponentInParent<ExtendedStarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput() // gestisco il click del pulsante per sparare
    {
        if (!extendedStarterAssetsInputs.shoot) return; // controllo se sparo
        // avvio l'animazione di sparo
        animator.Play(SHOOT_STRING, 0, 0);

        currentWeapon.Shoot(weaponConf); // chiamo il metodo Shoot() dell' arma corrente

        extendedStarterAssetsInputs.ShootInput(false); // resetto il flag dello sparo

    }
}
