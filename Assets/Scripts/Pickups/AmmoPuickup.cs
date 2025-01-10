using UnityEngine;

public class AmmoPuickup : Pickup
{
    [SerializeField] int ammoAmount = 100;

    protected override void OnPickup(ActiveWeapon aw)
    {
        aw.AdjustAmmo(ammoAmount);
    }
}
