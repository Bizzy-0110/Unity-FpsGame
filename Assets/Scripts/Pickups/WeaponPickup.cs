using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO; // arma del pickup
    [SerializeField] float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up*Time.deltaTime*rotationSpeed); // ruoto il modello
    }

    protected override void OnPickup(ActiveWeapon aw)
    {
        aw.SwitchWeapon(weaponSO);
    }
}
