using UnityEngine;

public class Weapon : MonoBehaviour
{
    ExtendedStarterAssetsInputs extendedStarterAssetsInputs;

    [SerializeField] int WeaponDamage = 1;

    private void Awake()
    {
        extendedStarterAssetsInputs = GetComponentInParent<ExtendedStarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }


    private void HandleInput()
    {
        if (!extendedStarterAssetsInputs.shoot) return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);

            HealtManager healtManage = hit.collider.GetComponent<HealtManager>();

            healtManage?.TakeDamage(WeaponDamage);

            extendedStarterAssetsInputs.ShootInput(false);
        }
    }
}
