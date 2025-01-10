using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using StarterAssets;
using TMPro;
using UnityEngine.Android;

public class ActiveWeapon : MonoBehaviour
{
    #region Attributes

    // costanti
    const string SHOOT_STRING = "Shoot"; // nome dell' animazione di sparo

    #region Serialized Fields


    [SerializeField] TMP_Text ammoText; // etichetta che contiene il numero di munizioni
    [SerializeField] GameObject zoomVignette; // vignetta che appare quando si usa lo zoom (arma con ottica)

    [SerializeField] CinemachineVirtualCamera playerFollowCamera; // riferimento alla camera 
    [SerializeField] WeaponSO startingWeaponSO; // Weapon Scriptable Object contenente le impostazioni dell'arma iniziale

    #endregion

    WeaponSO CurrentWeaponSO; // Scriptable Object contenente le impostazioni dell'arma corrente
    Weapon currentWeapon; // riferimento all'arma corrente
    

    private float timeSinceLastShot = float.MaxValue; // tempo trascorso dall'ultimo sparo
    private float defaultFOV; // fov predefinito della camera
    private float defaultRotationSpeed; // velocità default di rotazione della visuale
    private int currentAmmo = 0; // quantità munizioni correnti

    ExtendedStarterAssetsInputs extendedStarterAssetsInputs; // estensione della classe StarterAssetsInputs
    FirstPersonController firstPersonController; // riferimento al FirstPersonController
    Animator animator; // controller per l'animazione

    #endregion

    public void AdjustAmmo(int amount) // metodo per aggiungere munizioni
    {
        currentAmmo += amount;
        
        if (currentAmmo > CurrentWeaponSO.MagazineSize)
            currentAmmo = CurrentWeaponSO.MagazineSize;

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

    void Start()
    {
        //currentWeapon = GetComponentInChildren<Weapon>();

        SwitchWeapon(startingWeaponSO); // imposto l'arma iniziale
        AdjustAmmo(CurrentWeaponSO.MagazineSize); // imposto il numero di munizioni iniziale

    }

    void Update()
    {
        HandleShoot();
        HandleZoom();

        timeSinceLastShot += Time.deltaTime; // incremento il tempo trascorso dall'ultimo sparo
    }

    private void HandleZoom()
    {
        if (!CurrentWeaponSO.canZoom) return; // controllo che l'arma possa zoomare

        if (extendedStarterAssetsInputs.zoom) // controllo devo zoomare
        {
            //Debug.Log("zoom in");

            zoomVignette.SetActive(true); // rendo visibile la vignetta
            playerFollowCamera.m_Lens.FieldOfView = CurrentWeaponSO.ZoomFOV; // modifico il fov

            firstPersonController.RotationSpeed = CurrentWeaponSO.ZoomRotationSpeed; // imposto la rotationSpeed prendendola dalla condfigurazione dell'arma
        }
        else
        {

            // Debug.Log("zoom out");

            zoomVignette.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;

            firstPersonController.RotationSpeed = defaultRotationSpeed; // resetto la rotation speed
        }
    }

    private void HandleShoot() // gestisco il click del pulsante per sparare
    {
        if (!extendedStarterAssetsInputs.shoot) return; // controllo se sparo

        if(timeSinceLastShot < CurrentWeaponSO.FireRate || currentAmmo<=0) return; // controllo se è passato il tempo di shot e se ho proiettili

        timeSinceLastShot = 0f; // resetto il contatore

        // avvio l'animazione di sparo
        animator.Play(SHOOT_STRING, 0, 0);

        currentWeapon.Shoot(CurrentWeaponSO); // chiamo il metodo Shoot() dell' arma corrente

        AdjustAmmo(-1); // scalo il numero di proiettili

        if (!CurrentWeaponSO.isAutomatic)
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

        AdjustAmmo(weaponSO.MagazineSize);
    }
}
