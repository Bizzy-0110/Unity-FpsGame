using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO; // arma del pickup

    private void Update()
    {
        rotateModel(); // rotazione del modello del pickup
    }

    protected override void OnPickup(ActiveWeapon aw)
    {
        aw.SwitchWeapon(weaponSO);
    }
}
