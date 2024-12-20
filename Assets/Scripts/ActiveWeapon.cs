using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class ActiveWeapon : MonoBehaviour
{
    Weapon currentWeapon;

    private float timeSinceLastShot = float.MaxValue;
    private float defaultFOV;

    ExtendedStarterAssetsInputs extendedStarterAssetsInputs; // estensione della classe StarterAssetsInputs

    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] WeaponSO weaponConf; // Weapon Scriptable Object contenente le impostazioni dell'arma

    Animator animator; // controller per l'animazione

    const string SHOOT_STRING = "Shoot"; // nome dell' animazione di sparo

    private void Awake() // funzione che viene eseguata ancora prima della start()
    {
        extendedStarterAssetsInputs = GetComponentInParent<ExtendedStarterAssetsInputs>();
        animator = GetComponent<Animator>();

        defaultFOV = playerFollowCamera.m_Lens.FieldOfView; // ottengo la fov predefinito
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        HandleInput();
        HandleZoom();

        timeSinceLastShot += Time.deltaTime;
    }

    private void HandleZoom()
    {
        if (!weaponConf.canZoom) return; // controllo che l'arma possa sparare

        if (extendedStarterAssetsInputs.zoom)
        {
            Debug.Log("zoom in");
            zoomVignette.SetActive(true);
            playerFollowCamera.m_Lens.FieldOfView = weaponConf.ZoomFOV;
            
        }
        else
        {

            Debug.Log("zoom out");
            zoomVignette.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
        }
    }

    private void HandleInput() // gestisco il click del pulsante per sparare
    {
        if (!extendedStarterAssetsInputs.shoot) return; // controllo se sparo

        if(timeSinceLastShot < weaponConf.FireRate) return; // controllo se è passato il tempo di shot

        timeSinceLastShot = 0f; // resetto il contatore

        // avvio l'animazione di sparo
        animator.Play(SHOOT_STRING, 0, 0);

        currentWeapon.Shoot(weaponConf); // chiamo il metodo Shoot() dell' arma corrente
  
        if (!weaponConf.isAutomatic)
            extendedStarterAssetsInputs.ShootInput(false); // resetto il flag dello sparo

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Cambio arma " +  weaponSO.name);

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon weapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();

        currentWeapon = weapon;
        this.weaponConf = weaponSO;
    }
}
