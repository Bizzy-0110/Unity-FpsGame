using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using StarterAssets;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    Weapon currentWeapon;

    private float timeSinceLastShot = float.MaxValue;
    private float defaultFOV;
    private float defaultRotationSpeed; // velocit� default di rotazione della visuale

    ExtendedStarterAssetsInputs extendedStarterAssetsInputs; // estensione della classe StarterAssetsInputs

    [SerializeField] TMP_Text ammoText; // etichetta che contiene il numero di munizioni
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] WeaponSO startingWeaponSO; // Weapon Scriptable Object contenente le impostazioni dell'arma iniziale

    WeaponSO CurrentWeaponSO;
    FirstPersonController firstPersonController;

    private int currentAmmo;

    Animator animator; // controller per l'animazione

    const string SHOOT_STRING = "Shoot"; // nome dell' animazione di sparo

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        ammoText.text = currentAmmo.ToString("D2");
    }

    private void Awake() // funzione che viene eseguata ancora prima della start()
    {
        extendedStarterAssetsInputs = GetComponentInParent<ExtendedStarterAssetsInputs>();
        animator = GetComponent<Animator>();

        defaultFOV = playerFollowCamera.m_Lens.FieldOfView; // ottengo la fov predefinito
        firstPersonController = GetComponentInParent<FirstPersonController>(); // ottengo il first pearson controller

        defaultRotationSpeed = firstPersonController.RotationSpeed; // assegno la defaultRotationSpeed
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentWeapon = GetComponentInChildren<Weapon>();

        SwitchWeapon(startingWeaponSO);
        AdjustAmmo(CurrentWeaponSO.MagazineSize);

    }

    void Update()
    {
        HandleInput();
        HandleZoom();

        timeSinceLastShot += Time.deltaTime;
    }

    private void HandleZoom()
    {
        if (!startingWeaponSO.canZoom) return; // controllo che l'arma possa sparare

        if (extendedStarterAssetsInputs.zoom)
        {
            Debug.Log("zoom in");
            zoomVignette.SetActive(true); // rendo visibile la vignetta
            playerFollowCamera.m_Lens.FieldOfView = startingWeaponSO.ZoomFOV; // modifico il fov

            firstPersonController.RotationSpeed = startingWeaponSO.ZoomRotationSpeed; // imposto la rotationSpeed prendendola dalla condfigurazione dell'arma
        }
        else
        {

            Debug.Log("zoom out");
            zoomVignette.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;

            firstPersonController.RotationSpeed = defaultRotationSpeed; // resetto la rotation speed
        }
    }

    private void HandleInput() // gestisco il click del pulsante per sparare
    {
        if (!extendedStarterAssetsInputs.shoot) return; // controllo se sparo

        if(timeSinceLastShot < startingWeaponSO.FireRate) return; // controllo se � passato il tempo di shot

        timeSinceLastShot = 0f; // resetto il contatore

        // avvio l'animazione di sparo
        animator.Play(SHOOT_STRING, 0, 0);

        currentWeapon.Shoot(startingWeaponSO); // chiamo il metodo Shoot() dell' arma corrente
  
        if (!startingWeaponSO.isAutomatic)
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
        this.CurrentWeaponSO = weaponSO;
    }
}
