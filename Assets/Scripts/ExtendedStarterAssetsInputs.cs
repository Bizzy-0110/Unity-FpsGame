using StarterAssets;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ExtendedStarterAssetsInputs : StarterAssetsInputs
{

    // shooting
    public bool shoot;

    #if ENABLE_INPUT_SYSTEM
    public void OnShoot(InputValue inputValue)
    {
        ShootInput(inputValue.isPressed);
    }
    #endif

    public void ShootInput(bool newShootState)
    {
        shoot = newShootState;
    }
}
